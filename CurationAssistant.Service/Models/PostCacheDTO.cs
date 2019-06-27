using System;

namespace CurationAssistant.Service.Models
{    
    public class PostCacheDTO
    {
        public int post_id { get; set; }
        public string author { get; set; }
        public string permlink { get; set; }        
        public string category { get; set; }
        public int depth { get; set; }
        public int children { get; set; }
        public Single author_rep { get; set; }
        public Single flag_weight { get; set; }
        public int total_votes { get; set; }
        public int up_votes { get; set; }
        public string title { get; set; }
        public string preview { get; set; }
        public string img_url { get; set; }
        public decimal payout { get; set; }
        public decimal promoted { get; set; }
        public DateTime created_at { get; set; }
        public DateTime payout_at { get; set; }
        public DateTime updated_at { get; set; }
        public bool is_paidout { get; set; }
        public bool is_nsfw { get; set; }
        public bool is_declined { get; set; }
        public bool is_full_power { get; set; }
        public bool is_hidden { get; set; }
        public bool is_grayed { get; set; }
        public Int64 rshares { get; set; }
        public Single sc_trend { get; set; }
        public Single sc_hot { get; set; }
        public string body { get; set; }
        public string votes { get; set; }
        public string json { get; set; }
        public string raw_json { get; set; }
    }
}
