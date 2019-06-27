using System;

namespace CurationAssistant.Service.Models
{
    public class AccountDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public DateTime created_at { get; set; }
        public Single reputation { get; set; }
        public string display_name { get; set; }
        public string about { get; set; }
        public string location { get; set; }
        public string website { get; set; }
        public string profile_image { get; set; }
        public string cover_image { get; set; }
        public int followers { get; set; }
        public int following { get; set; }
        public string proxy { get; set; }
        public int post_count { get; set; }
        public Single proxy_weight { get; set; }
        public Single vote_weight { get; set; }
        public int kb_used { get; set; }
        public int rank { get; set; }
        public DateTime active_at { get; set; }
        public DateTime cached_at { get; set; }
        public string raw_json { get; set; }
    }
}
