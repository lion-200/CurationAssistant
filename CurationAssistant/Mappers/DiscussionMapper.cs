using CurationAssistant.Helpers;
using CurationAssistant.Models;
using CurationAssistant.Models.SteemModels;
using CurationAssistant.Models.SteemModels.Partials;
using CurationAssistant.Models.TransactionHistory;
using CurationAssistant.Service.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurationAssistant.Mappers
{
    public static class DiscussionMapper
    {
        public static DiscussionListViewModel ToDiscussionListViewModel(GetDiscussionModel input, string accountName)
        {
            var output = new DiscussionListViewModel();
            output.Author = input.author;

            // it is a Resteem if the author of the post is not the account itself
            output.IsResteem = output.Author != accountName;

            // Deserialize json_metadata to get main image
            var jsonMetaData = JsonConvert.DeserializeObject<DiscussionJsonMetadata>(input.json_metadata);

            // set first image as main image
            if (jsonMetaData.image != null && jsonMetaData.image.Any())
            {
                output.MainImage = jsonMetaData.image.FirstOrDefault();
            }

            if (!output.IsResteem)
            {
                output.PendingPayout = CalculationHelper.ParsePayout(input.pending_payout_value);
                // if pending payout = 0, it might be the case that it is already paid out, so get the paid out value
                if (output.PendingPayout == 0)
                {
                    output.PaidOut = CalculationHelper.ParsePayout(input.total_payout_value);
                    output.PaidOutTotal = output.PaidOut + CalculationHelper.ParsePayout(input.curator_payout_value);
                }
            }

            output.Permlink = input.permlink;
            output.ParentPermlink = input.parent_permlink;
            output.Title = input.title;
            output.CreatedAt = input.created;
            output.PostPayoutDate = output.CreatedAt.AddDays(7);

            return output;
        }

        public static FindVotesItemModel ToFindVotesItemModel(PostCacheDTO post, string voter)
        {
            var model = new FindVotesItemModel();
            model.author = post.author;
            model.permlink = post.permlink;
            model.voter = voter;

            if (!String.IsNullOrEmpty(post.votes))
            {
                var votersList = post.votes.Split('\n').ToList();
                if (votersList != null && votersList.Any())
                {                    
                    var voterItem = votersList.FirstOrDefault(x => x.Contains(voter));

                    if (voterItem != null)
                    {
                        var voteDetailsList = voterItem.Split(',').ToList();

                        if(voteDetailsList != null && voteDetailsList.Any())
                        {
                            // 0 = voter
                            // 1 = rshares
                            // 2 = percentage
                            // 3 = weight
                            model.rshares = voteDetailsList[1];                            

                            int perc = 0;
                            Int32.TryParse(voteDetailsList[2], out perc);

                            model.vote_percent = perc;
                            model.weight = voteDetailsList[3];

                            // for all vote details, first map post creation date to vote date
                            model.last_update = post.created_at;
                        }
                    }
                }
            }
            

            return model;
        }
    }
}