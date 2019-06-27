using System;

namespace CurationAssistant.Service.Models
{
    public class ReBlogDTO
    {
        public string account { get; set; }
        public int post_id { get; set; }
        public DateTime created_at { get; set; }
    }
}
