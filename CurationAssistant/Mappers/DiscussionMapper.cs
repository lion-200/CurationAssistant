using CurationAssistant.Helpers;
using CurationAssistant.Models.SteemModels;
using CurationAssistant.Models.SteemModels.Partials;
using CurationAssistant.Models.TransactionHistory;
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
                    var totalPayout = CalculationHelper.ParsePayout(input.total_payout_value);
                    var curatorPayout = CalculationHelper.ParsePayout(input.curator_payout_value);

                    // we are only interested in the author payout, substract the curator payout from total payuout
                    output.PaidOut = totalPayout - curatorPayout;
                }
            }

            output.Permlink = input.permlink;
            output.ParentPermlink = input.parent_permlink;
            output.Title = input.title;
            output.CreatedAt = input.created;            

            return output;
        }
    }
}