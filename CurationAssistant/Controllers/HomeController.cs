using CurationAssistant.Helpers;
using CurationAssistant.Models;
using CurationAssistant.Models.SteemModels;
using CurationAssistant.Models.SteemModels.Partials;
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

        public ActionResult Index()
        {
            var model = new HomeViewModel();

            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ValidatePost(HomeViewModel model)
        {
            var result = new CurationDetailsViewModel();

            try
            {
                var linkItems = model.PostLink.Split('/');
                var accountName = linkItems.FirstOrDefault(x => x.Contains("@"));
                var permlink = linkItems.LastOrDefault();

                accountName = accountName.Replace("@", "");
                result.Author = GetAuthor(accountName);

                // get history 
                GetAccountHistoryDetails(accountName, result);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

            return Json(result);
        }

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

        private void GetAccountHistoryDetails(string accountName, CurationDetailsViewModel result)
        {
            try
            {
                var limit = Convert.ToInt32(ConfigurationHelper.HistoryTransactionLimit);
                uint batchSize = 1000;
                var start = -1;

                int transactionsRetrieved = 0;
                log.Info(string.Format("Batchsize: {0}", batchSize));

                using (var csteemd = new CSteemd(ConfigurationHelper.HostName))
                {
                    while (transactionsRetrieved < limit)
                    {
                        log.Info(string.Format("Retrieving next batch...Retrieved transaction count: {0}. Value start: {1}", transactionsRetrieved, start));

                        var responseHistory = csteemd.get_account_history(accountName, start, batchSize);
                        //log.Info(responseHistory.ToString());

                        // store last transaction datetime
                        result.LastTransactionDate = responseHistory[0][1]["timestamp"].ToObject<DateTime>();
                        log.Info(string.Format("Stored last transaction datetime: {0}", result.LastTransactionDate.ToString("dd-MM-yyyy HH:mm")));

                        var totalCount = responseHistory.Count();
                        for (var i = totalCount - 1; i >= 0; i--)
                        {
                            var el = responseHistory[i];

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
                                if (String.IsNullOrEmpty(operationModel.parent_author)) // post
                                {
                                    actionViewModel.Type = "post";
                                    actionViewModel.Details = operationModel;

                                    if (result.Posts.Count < ConfigurationHelper.PostTransactionCount)
                                    {
                                        result.Posts.Add(actionViewModel);
                                    }
                                }
                                else
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

                            if (result.Comments.Count == ConfigurationHelper.CommentTransactionCount &&
                                //result.Votes.Count == ConfigurationHelper.VoteTransactionCount &&
                                result.Posts.Count == ConfigurationHelper.PostTransactionCount)
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
    }
}