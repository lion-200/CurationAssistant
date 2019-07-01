using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurationAssistant.Models
{
    public class ValidationVariables
    {
        public int PostCreatedAtMin { get; set; }
        public int PostCreatedAtMax { get; set; }
        public decimal PostMaxPendingPayout { get; set; }
        public decimal AuthorRepMin { get; set; }
        public decimal AuthorRepMax { get; set; }
        public int PostsMin { get; set; }
        public int CommentsMin { get; set; }
        public int PostsMinDays { get; set; }
        public int CommentsMinDays { get; set; }
        public string UpvoteAccount { get; set; }
        public decimal VPMinRequired { get; set; }
        public decimal TotalMaxPendingPayout { get; set; }
        public decimal MaxPostPayoutAmount { get; set; }
        public int MaxPostPayoutDays { get; set; }
        public int MinDaysLastUpvoteFromUpvoteAccount { get; set; }
        public int MinPercentageUpvoteFromUpvoteAccount { get; set; }

        public ValidationVariables()
        {
            PostCreatedAtMin = 30; // in minutes
            PostCreatedAtMax = 4320; // in minutes corresponding to 72 hours
            PostMaxPendingPayout = 2; // in SBD
            AuthorRepMin = 25;
            AuthorRepMax = 100;
            PostsMin = 5;
            CommentsMin = 5;
            PostsMinDays = 7;
            CommentsMinDays = 7;
            UpvoteAccount = "curie";
            VPMinRequired = 88;
            TotalMaxPendingPayout = 10;
            MaxPostPayoutAmount = 10;
            MaxPostPayoutDays = 21;
            MinDaysLastUpvoteFromUpvoteAccount = 21;
            MinPercentageUpvoteFromUpvoteAccount = 30;
        }
    }
}