using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurationAssistant.Models.SteemModels
{
    public class FindVotesItemModel
    {
        public int id { get; set; }
        public string voter { get; set; }
        public string author { get; set; }
        public string permlink { get; set; }
        public string weight { get; set; }
        public string rshares { get; set; }
        public int vote_percent { get; set; }
        public DateTime last_update { get; set; }
        public int num_changes { get; set; }
    }
}