using AutoMapper;
using CurationAssistant.Helpers;
using CurationAssistant.Mappers;
using CurationAssistant.Models;
using CurationAssistant.Models.SteemModels;
using CurationAssistant.Models.SteemModels.Partials;
using CurationAssistant.Models.TransactionHistory;
using CurationAssistant.Service.ServiceInterfaces;
using Newtonsoft.Json;
using SteemAPI.CS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CurationAssistant.Controllers
{
    public class HomeController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(HomeController));
        private readonly IBlockService _blockService;        

        public HomeController(IBlockService blockService)
        {            
            _blockService = blockService;
        }

        public ActionResult Index()
        {
            var block = _blockService.GetMostRecentBlock();

            var model = new HomeViewModel();

            return View(model);
        }

        /// <summary>
        /// This method runs all validation rules and returns a summary of the results & rules
        /// </summary>
        /// <param name="model">The model containing already retrieved results like Author details, Post details etc. used to run validation on.</param>
        /// <param name="vars">The validation variables being used to run the validation on the data</param>
        /// <returns>Returns the model containing validation results</returns>
        public ValidationSummaryViewModel RunValidation(CurationDetailsViewModel model, ValidationVariables vars)
        {
            var result = new ValidationSummaryViewModel();

            var validationItems = new List<ValidationItemViewModel>();
            try
            {
                validationItems.Add(ValidationHelper.ValidatePostCreateDateRule(model, vars));
                validationItems.Add(ValidationHelper.ValidatePostMaxPendingPayoutRule(model, vars));
                validationItems.Add(ValidationHelper.ValidateTotalMaxPendingPayoutRule(model, vars));
                validationItems.Add(ValidationHelper.ValidateMaxPostPayoutRule(model, vars));
                validationItems.Add(ValidationHelper.ValidateAuthorReputationRule(model, vars));
                validationItems.Add(ValidationHelper.ValidateMinPostsRule(model, vars));
                validationItems.Add(ValidationHelper.ValidateMinCommentsRule(model, vars));

                var upvoteAccountDetails = GetAccountDetails(vars.UpvoteAccount.Replace("@", ""));
                validationItems.Add(ValidationHelper.ValidateUpvoteAccountMinVPRule(upvoteAccountDetails, vars));
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

            result.Items = validationItems;

            return result;
        }

        /// <summary>
        /// This method processes the input request containing the post link and calls the appropriate methods to retrieve data necessary
        /// </summary>
        /// <param name="model">The model containing the link of the post and validation variables possibly edited by the user</param>
        /// <returns>Returns the data retrieved and validation summary</returns>
        [HttpPost]
        public ActionResult ValidatePost(HomeViewModel model)
        {
            var result = new CurationDetailsViewModel();

            try
            {
                // split link into parts to get account name
                var linkItems = model.PostLink.Split('/');
                var accountName = linkItems.FirstOrDefault(x => x.Contains("@"));
                var permlink = linkItems.LastOrDefault();

                accountName = accountName.Replace("@", "");

                // get account details
                result.Author = GetAuthor(accountName);

                // get account history details
                GetAccountHistoryDetails(accountName, result);

                // get_discussion
                result.BlogPost = GetBlogPost(accountName, permlink);

                if (result.BlogPost != null)
                {
                    // validate data
                    result.ValidationSummary = RunValidation(result, model.ValidationVariables);
                }

                // return only a subset of posts
                result.Posts = result.Posts.Take(ConfigurationHelper.PostTransactionCount).ToList();
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

            return Json(result);
        }

        /// <summary>
        /// This method gets the account details and enriches the data with some calculated values like Reputation, SteemPower etc.
        /// </summary>
        /// <param name="accountName">The name of the account</param>
        /// <returns>Returns the enriched <see cref="AuthorViewModel" /> class containing data from API and calculated values</returns>
        private AuthorViewModel GetAuthor(string accountName)
        {
            var author = new AuthorViewModel();
            try
            {
                var globalProperties = GetDynamicGlobalProperties();
                var steemPerMVests = CalculationHelper.CalculateSteemPerMVests(globalProperties);
                author.Details = GetAccountDetails(accountName);
                author.ReputationCalculated = CalculationHelper.CalculateReputation(author.Details.reputation);
                author.SteemPowerCalculated = CalculationHelper.CalculateSteemPower(author.Details.vesting_shares, steemPerMVests);
                author.VotingManaPercentageCalculated = CalculationHelper.CalculateVotingManaPercentage(author.Details);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

            return author;
        }

        /// <summary>
        /// This method gets the dynamic global properties from Steem
        /// </summary>
        /// <returns>Returns the retrieved data mapped to <see cref="GetDynamicGlobalPropertiesModel" /> class</returns>
        private GetDynamicGlobalPropertiesModel GetDynamicGlobalProperties()
        {
            var model = new GetDynamicGlobalPropertiesModel();

            try
            {
                using (var csteemd = new CSteemd(ConfigurationHelper.HostName))
                {
                    var response = csteemd.get_dynamic_global_properties();
                    if (response != null)
                    {
                        var result = response.ToObject<GetDynamicGlobalPropertiesModel>();
                        if (result != null)
                        {
                            model = response.ToObject<GetDynamicGlobalPropertiesModel>();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

            return model;
        }

        /// <summary>
        /// This method calls the getAccounts method from Steem
        /// </summary>
        /// <param name="accountName">The name of the account</param>
        /// <returns>Returns the results mapped to <see cref="GetAccountsModel" /> class</returns>
        private GetAccountsModel GetAccountDetails(string accountName)
        {
            var account = new GetAccountsModel();

            try
            {
                if (!String.IsNullOrEmpty(accountName))
                {
                    using (var csteemd = new CSteemd(ConfigurationHelper.HostName))
                    {
                        // account details
                        var response = csteemd.get_accounts(new ArrayList() { accountName });
                        if (response != null)
                        {
                            var accountResult = response.ToObject<List<GetAccountsModel>>();
                            account = accountResult.FirstOrDefault();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

            return account;
        }

        /// <summary>
        /// This method makes subsequent calls to get_account_history on Steem in order to collect sufficient amount of data to run validation on
        /// The batchSize parameter defines the limit of the transactions we want to be returned from Steem API.
        /// The web.config setting HistoryTransactionLimit contains the max value of transactions we set in order to prevent calling the Steem API a lot of times
        /// Based on the "op" variable in the returned transactions, the values will be mapped to corresponding classes and added to result set to return.
        /// </summary>
        /// <param name="accountName">The name of the account</param>
        /// <param name="result">The result set to be enriched with transaction data</param>
        private void GetAccountHistoryDetails(string accountName, CurationDetailsViewModel result)
        {
            try
            {
                // get posts
                var posts = GetAccountPosts(accountName);
                if (posts != null && posts.Any())
                    result.LastRetrievedPostDate = posts.LastOrDefault().CreatedAt;                

                result.Author.PendingPostPayout = CalculationHelper.CalculatePendingPostPayout(accountName, posts);

                result.Posts = posts;

                var limit = Convert.ToInt32(ConfigurationHelper.HistoryTransactionLimit);
                uint batchSize = 1000;
                var start = -1;

                int transactionsRetrieved = 0;
                log.Info(string.Format("Batchsize: {0}", batchSize));

                using (var csteemd = new CSteemd(ConfigurationHelper.HostName))
                {
                    // stop if the max amount of transactions are reached!
                    while (transactionsRetrieved < limit)
                    {
                        log.Info(string.Format("Retrieving next batch...Retrieved transaction count: {0}. Value start: {1}", transactionsRetrieved, start));

                        var responseHistory = csteemd.get_account_history(accountName, start, batchSize);                        

                        // store last transaction datetime, so that we know until what data time value we got the transactions
                        result.LastTransactionDate = responseHistory[0][1]["timestamp"].ToObject<DateTime>();
                        log.Info(string.Format("Stored last transaction datetime: {0}", result.LastTransactionDate.ToString("dd-MM-yyyy HH:mm")));
                                                
                        var totalCount = responseHistory.Count();
                        // get_account_history returns last result first, but we want most recent first, so we start from the last element of the response to loop
                        for (var i = totalCount - 1; i >= 0; i--)
                        {
                            var el = responseHistory[i];

                            // get the index of the last transaction in the list to make the next call start from this index
                            if (transactionsRetrieved == 0)
                            {
                                var firstIndex = el[0].ToString();
                                Int32.TryParse(firstIndex, out start);
                            }

                            var transaction = el[1].ToObject<TransactionModel>();

                            var operation = el[1]["op"];
                            var operationType = operation[0].ToString();

                            var actionViewModel = new ActionViewModel();
                            actionViewModel.TimeStamp = el[1]["timestamp"].ToObject<DateTime>();
                            if (operationType == "vote" && result.Votes.Count < ConfigurationHelper.VoteTransactionCount)
                            {
                                var operationModel = operation[1].ToObject<OperationVoteViewModel>();

                                if (operationModel.voter == accountName)
                                {
                                    actionViewModel.Type = "vote";
                                    actionViewModel.Details = operationModel;

                                    result.Votes.Add(actionViewModel);
                                }
                            }
                            else if (operationType == "comment")
                            {
                                var operationModel = operation[1].ToObject<OperationCommentViewModel>();
                                if (!String.IsNullOrEmpty(operationModel.parent_author)) // post
                                {
                                    actionViewModel.Type = "comment";
                                    actionViewModel.Details = operationModel;

                                    if (result.Comments.Count < ConfigurationHelper.CommentTransactionCount &&
                                        operationModel.author == accountName)
                                    {
                                        result.Comments.Add(actionViewModel);
                                    }
                                }
                            }

                            // if the required amount of counts are reached, stop
                            if (result.Comments.Count == ConfigurationHelper.CommentTransactionCount)
                            {
                                break;
                            }
                        }

                        transactionsRetrieved += (int)batchSize;
                        start -= (int)batchSize;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        /// <summary>
        /// This method calls the get_discussions_by_blog method from Steem API and returns the mapped list of <see cref="DiscussionListViewModel" /> models
        /// </summary>
        /// <param name="accountName">The name of the account to get posts for</param>
        /// <returns>List of <see cref="DiscussionListViewModel" /> models</returns>
        private List<DiscussionListViewModel> GetAccountPosts(string accountName)
        {
            var postCountToGet = 30;
            var posts = new List<DiscussionListViewModel>();

            try
            {
                using (var csteemd = new CSteemd(ConfigurationHelper.HostName))
                {
                    var steemResponse = csteemd.get_discussions_by_blog(accountName, postCountToGet);
                    var discussionList = new List<GetDiscussionModel>();

                    if(steemResponse != null && steemResponse.Count > 0)
                    {
                        foreach(var item in steemResponse)
                        {
                            var disc = item.ToObject<GetDiscussionModel>();
                            var post = DiscussionMapper.ToDiscussionListViewModel(disc, accountName);

                            posts.Add(post);
                        }
                    }                    
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

            return posts;
        }

        /// <summary>
        /// This method calls the Steem API to get the post details 
        /// </summary>
        /// <param name="accountName">The name of the account</param>
        /// <param name="permlink">The permLink of the post</param>
        /// <returns>Returns the post details enriched with calculated values</returns>
        private BlogPostViewModel GetBlogPost(string accountName, string permlink)
        {
            var blogPost = new BlogPostViewModel();
            try
            {
                blogPost.Details = GetDiscussion(accountName, permlink);

                if (blogPost.Details != null)
                {
                    blogPost.WordCount = CalculationHelper.GetWordCount(blogPost.Details.body);

                    blogPost.DiscussionMetadata = JsonConvert.DeserializeObject<DiscussionJsonMetadata>(blogPost.Details.json_metadata);
                    if (blogPost.DiscussionMetadata != null &&
                        blogPost.DiscussionMetadata.image != null &&
                        blogPost.DiscussionMetadata.image.Any())
                    {
                        blogPost.ImageCount = blogPost.DiscussionMetadata.image.Count;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }


            return blogPost;
        }

        /// <summary>
        /// This method gets the post details response from Steem API
        /// </summary>
        /// <param name="accountName">The name of the account</param>
        /// <param name="permlink">The permlink of the post</param>
        /// <returns>Returns the result mapped to <see cref="GetDiscussionModel" /></returns>
        private GetDiscussionModel GetDiscussion(string accountName, string permlink)
        {
            var blogPost = new GetDiscussionModel();

            try
            {
                using (var csteemd = new CSteemd(ConfigurationHelper.HostName))
                {
                    var response = csteemd.get_discussion(accountName, permlink);
                    if (response != null)
                    {
                        blogPost = response.ToObject<GetDiscussionModel>();
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

            return blogPost;
        }

    }
}