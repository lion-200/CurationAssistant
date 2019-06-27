using System;

namespace CurationAssistant.Service.Models
{
    public class PostDTO
    {
        public int id { get; set; }
        public int parent_id { get; set; }
        public string author { get; set; }
        public string permlink { get; set; }
        public string community { get; set; }
        public string category { get; set; }
        public int depth { get; set; }
        public DateTime created_at { get; set; }
        public bool is_deleted { get; set; }
        public bool is_muted { get; set; }
        public bool is_valid { get; set; }
        public bool is_pinned { get; set; }
        public decimal promoted { get; set; }
    }
}
