using System;

namespace CurationAssistant.Service.Models
{
    public class CommunityDTO
    {
        public string name { get; set; }
        public string title { get; set; }
        public string about { get; set; }
        public string description { get; set; }
        public string lang { get; set; }
        public string settings { get; set; }
        public int type_id { get; set; }
        public bool is_nsfw { get; set; }
        public DateTime created_at { get; set; }
    }
}
