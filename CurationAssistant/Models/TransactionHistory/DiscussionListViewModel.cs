using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurationAssistant.Models.TransactionHistory
{
    public class DiscussionListViewModel
    {
        public string Permlink { get; set; }
        public string ParentPermlink { get; set; }
        public decimal PendingPayout { get; set; }
        public decimal PaidOut { get; set; }
        public string Title { get; set; }
        public string MainImage { get; set; }        
        public DateTime CreatedAt { get; set; }
        public string Author { get; set; }
        public bool IsResteem { get; set; }
    }
}