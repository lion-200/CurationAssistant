using log4net;
using CurationAssistant.Models;
using CurationAssistant.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CurationAssistant.Models.SteemModels;

namespace CurationAssistant.Helpers
{
    /// <summary>
    /// Helper class containing Validation methods
    /// </summary>
    public static class ValidationHelper
    {
        public static ValidationItemViewModel ValidatePostCreateDateRule(CurationDetailsViewModel model, ValidationVariables vars)
        {
            ValidationPriority prio = ValidationPriority.High;
            ValidationResultType resultType = ValidationResultType.Failure;

            var validationItem = new ValidationItemViewModel();
            validationItem.Title = string.Format("Post creation date is > {0} minutes and < {1} hours", vars.PostCreatedAtMin, vars.PostCreatedAtMax / 60);
            validationItem.Priority = prio;
            validationItem.PriorityDescription = prio.ToString();
            validationItem.OrderId = 1;

            var postCreatedDate = model.BlogPost.Details.created;

            // Check if the post creation date is between the required ranges
            if (DateTime.Now > postCreatedDate.AddMinutes(vars.PostCreatedAtMin) &&
                DateTime.Now < postCreatedDate.AddMinutes(vars.PostCreatedAtMax))
            {
                resultType = ValidationResultType.Success;
            }
            else
            {
                resultType = ValidationResultType.Failure;
            }

            validationItem.ResultType = resultType;
            validationItem.ResultTypeDescription = resultType.ToString();

            var span = (TimeSpan)(DateTime.Now - postCreatedDate);
            validationItem.ResultMessage = string.Format("Post created {0} days, {1} hours, {2} minutes ago.", span.Days, span.Hours, span.Minutes);

            return validationItem;
        }

        public static ValidationItemViewModel ValidatePostMaxPendingPayoutRule(CurationDetailsViewModel model, ValidationVariables vars)
        {
            var validationItem = new ValidationItemViewModel();

            ValidationPriority prio = ValidationPriority.High;
            ValidationResultType resultType = ValidationResultType.Failure;

            validationItem.Title = string.Format("Post max pending payout value < {0}", vars.PostMaxPendingPayout);
            validationItem.Priority = prio;
            validationItem.PriorityDescription = prio.ToString();
            validationItem.OrderId = 2;

            decimal postPendingPayoutValue = 0;
            var postPendingPayoutString = model.BlogPost.Details.pending_payout_value.Replace("SBD", "").Trim();
            Decimal.TryParse(postPendingPayoutString, out postPendingPayoutValue);

            // check if the pending payout value of the post is less than the max payout setting
            if (postPendingPayoutValue < vars.PostMaxPendingPayout)
            {
                resultType = ValidationResultType.Success;
            }
            else
            {
                resultType = ValidationResultType.Failure;
            }

            validationItem.ResultType = resultType;
            validationItem.ResultTypeDescription = resultType.ToString();

            validationItem.ResultMessage = string.Format("Post pending payout value = ${0}", postPendingPayoutValue.ToString("N"));        

            return validationItem;
        }

        public static ValidationItemViewModel ValidateTotalMaxPendingPayoutRule(CurationDetailsViewModel model, ValidationVariables vars)
        {
            var validationItem = new ValidationItemViewModel();

            ValidationPriority prio = ValidationPriority.High;
            ValidationResultType resultType = ValidationResultType.Failure;

            validationItem.Title = string.Format("Total max pending payout value < {0}", vars.TotalMaxPendingPayout);
            validationItem.Priority = prio;
            validationItem.PriorityDescription = prio.ToString();
            validationItem.OrderId = 3;            
            
            // if the oldest post retrieved is a more recent post than 1 week before current date, we don't have enough data to validate this rule
            var dateCheck = DateTime.Now.AddDays(-7);
            if (model.LastRetrievedPostDate > dateCheck)
            {
                if(model.Author.PendingPostPayout > vars.TotalMaxPendingPayout)
                {
                    resultType = ValidationResultType.Failure;
                } else
                {
                    resultType = ValidationResultType.Neutral;
                    validationItem.ResultMessage = string.Format("Dataset retrieved does not contain enough data to validate this rule. Oldest retrieved post date in data set is {0}", model.LastRetrievedPostDate.ToString("yyyy-MM-dd HH:mm"));
                }
            }
            else
            {
                if (model.Author.PendingPostPayout <= vars.TotalMaxPendingPayout)
                {
                    resultType = ValidationResultType.Success;
                }
                else
                {
                    resultType = ValidationResultType.Failure;
                }
            }

            validationItem.ResultType = resultType;
            validationItem.ResultTypeDescription = resultType.ToString();

            if(String.IsNullOrEmpty(validationItem.ResultMessage))
                validationItem.ResultMessage = string.Format("Total pending payout value = ${0}", model.Author.PendingPostPayout.ToString("N"));

            return validationItem;
        }

        public static ValidationItemViewModel ValidateAuthorReputationRule(CurationDetailsViewModel model, ValidationVariables vars)
        {
            ValidationPriority prio = ValidationPriority.High;
            ValidationResultType resultType = ValidationResultType.Failure;

            var validationItem = new ValidationItemViewModel();
            validationItem.Title = string.Format("Author reputation is > {0} and < {1}", vars.AuthorRepMin, vars.AuthorRepMax);
            validationItem.Priority = prio;
            validationItem.PriorityDescription = prio.ToString();
            validationItem.OrderId = 3;
                        
            // Check if the author rep value is within the required range
            if (model.Author.ReputationCalculated > vars.AuthorRepMin &&
                model.Author.ReputationCalculated < vars.AuthorRepMax)
            {
                resultType = ValidationResultType.Success;
            }
            else
            {
                resultType = ValidationResultType.Failure;
            }

            validationItem.ResultType = resultType;
            validationItem.ResultTypeDescription = resultType.ToString();                        
            validationItem.ResultMessage = string.Format("Author reputation is {0}", model.Author.ReputationCalculated.ToString("N"));

            return validationItem;
        }

        public static ValidationItemViewModel ValidateMinPostsRule(CurationDetailsViewModel model, ValidationVariables vars)
        {
            ValidationPriority prio = ValidationPriority.High;
            ValidationResultType resultType = ValidationResultType.Failure;

            var validationItem = new ValidationItemViewModel();
            validationItem.Title = string.Format("Required minimum # posts {0} in last {1} days", vars.PostsMin, vars.PostsMinDays);
            validationItem.Priority = prio;
            validationItem.PriorityDescription = prio.ToString();
            validationItem.OrderId = 4;

            // get posts within range
            var dateCheck = DateTime.Now.AddDays(-vars.PostsMinDays);
            var postCount = model.Posts.Count(x => x.CreatedAt >= dateCheck);
            
            if (postCount >= vars.PostsMin)
            {
                resultType = ValidationResultType.Success;
                validationItem.ResultMessage = string.Format("Author posted {0} posts in last {1} days.", postCount, vars.PostsMinDays);
            }
            else
            {
                if(model.LastTransactionDate > dateCheck)
                {
                    resultType = ValidationResultType.Neutral;
                    validationItem.ResultMessage = string.Format("Dataset retrieved does not contain enough data to validate this rule. Oldest transaction date in data set is {0}", model.LastTransactionDate.ToString("yyyy-MM-dd HH:mm"));
                } else
                {
                    resultType = ValidationResultType.Failure;
                    validationItem.ResultMessage = string.Format("Author posted {0} posts in last {1} days.", postCount, vars.PostsMinDays);
                }                
            }

            validationItem.ResultType = resultType;
            validationItem.ResultTypeDescription = resultType.ToString();                                    

            return validationItem;
        }

        public static ValidationItemViewModel ValidateMinCommentsRule(CurationDetailsViewModel model, ValidationVariables vars)
        {
            ValidationPriority prio = ValidationPriority.High;
            ValidationResultType resultType = ValidationResultType.Failure;

            var validationItem = new ValidationItemViewModel();
            validationItem.Title = string.Format("Required minimum # comments {0} in last {1} days", vars.CommentsMin, vars.CommentsMinDays);
            validationItem.Priority = prio;
            validationItem.PriorityDescription = prio.ToString();
            validationItem.OrderId = 5;

            // get comments within range
            var dateCheck = DateTime.Now.AddDays(-vars.CommentsMinDays);
            var commentCount = model.Comments.Count(x => x.TimeStamp >= dateCheck);

            if (commentCount > vars.CommentsMin)
            {
                resultType = ValidationResultType.Success;
                validationItem.ResultMessage = string.Format("Author posted {0} comments in last {1} days.", commentCount, vars.CommentsMinDays);
            }
            else
            {
                if (model.LastTransactionDate > dateCheck)
                {
                    resultType = ValidationResultType.Neutral;
                    validationItem.ResultMessage = string.Format("Dataset retrieved does not contain enough data to validate this rule. Oldest transaction date in data set is {0}", model.LastTransactionDate.ToString("yyyy-MM-dd HH:mm"));
                }
                else
                {
                    resultType = ValidationResultType.Failure;
                    validationItem.ResultMessage = string.Format("Author posted {0} comments in last {1} days.", commentCount, vars.CommentsMinDays);
                }
            }

            validationItem.ResultType = resultType;
            validationItem.ResultTypeDescription = resultType.ToString();

            return validationItem;
        }

        public static ValidationItemViewModel ValidateUpvoteAccountMinVPRule(GetAccountsModel accountDetails, ValidationVariables vars)
        {
            ValidationPriority prio = ValidationPriority.High;
            ValidationResultType resultType = ValidationResultType.Failure;

            var validationItem = new ValidationItemViewModel();
            validationItem.Title = string.Format("Minimum required VP for account {0} is {1} %", vars.UpvoteAccount, vars.VPMinRequired);
            validationItem.Priority = prio;
            validationItem.PriorityDescription = prio.ToString();
            validationItem.OrderId = 6;

            // get comments within range
            var vpCalculated = CalculationHelper.CalculateVotingManaPercentage(accountDetails);

            if (vpCalculated > vars.VPMinRequired)
            {
                resultType = ValidationResultType.Success;
                validationItem.ResultMessage = string.Format("VP of account {0} is {1} %.", vars.UpvoteAccount, vpCalculated.ToString("N"));
            }
            else
            {
                resultType = ValidationResultType.Failure;
                validationItem.ResultMessage = string.Format("VP of account {0} is {1} %.", vars.UpvoteAccount, vpCalculated.ToString("N"));
            }

            validationItem.ResultType = resultType;
            validationItem.ResultTypeDescription = resultType.ToString();

            return validationItem;
        }
    }
}