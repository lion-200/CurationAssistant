using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurationAssistant.Models.TransactionHistory
{
    public class GetAccountVotesViewModel
    {
        public DateTime LastTransactionDate { get; set; }
        public int VotesAnalyzed { get; set; }
        public ActionViewModel LastVote { get; set; }
    }
}