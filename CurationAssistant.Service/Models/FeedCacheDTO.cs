using System;

namespace CurationAssistant.Service.Models
{
    public class FeedCacheDTO
    {
        public int post_id { get; set; }
        public int account_id { get; set; }
        public DateTime created_at { get; set; }
    }
}
