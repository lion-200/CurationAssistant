using CurationAssistant.Models.SteemModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurationAssistant.Models.TransactionHistory
{
    public class GetAccountVotesViewModel
    {
        public int MinPercentage { get; set; }
        public List<FindVotesItemModel> LastVotes { get; set; }
        public FindVotesItemModel MostRecentUpvote
        {
            get
            {
                if (LastVotes != null && LastVotes.Any())
                {
                    // we multiply MinPercentage with 100 because the percentage is stored that way in Steem Blockchain. 
                    // Example: 50% is stored as 5000
                    var vote = LastVotes.Where(x => x.vote_percent >= MinPercentage * 100).FirstOrDefault();

                    return vote;
                }

                return null;
            }
        }

        public GetAccountVotesViewModel()
        {
            LastVotes = new List<FindVotesItemModel>();
        }
    }
}