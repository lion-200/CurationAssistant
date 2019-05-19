using CurationAssistant.Models.SteemModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurationAssistant.Models
{
    public class AuthorViewModel
    {
        public GetAccountsModel Details { get; set; }
        public decimal SteemPowerCalculated { get; set; }
        public decimal ReputationCalculated { get; set; }
        public decimal VotingManaPercentageCalculated { get; set; }
    }
}