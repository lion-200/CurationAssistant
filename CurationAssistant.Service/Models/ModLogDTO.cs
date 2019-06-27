using System;

namespace CurationAssistant.Service.Models
{
    public class ModLogDTO
    {
        public int id { get; set; }
        public string community { get; set; }
        public string account { get; set; }
        public string action { get; set; }
        public string parameters { get; set; }
        public DateTime created_at { get; set; }
    }
}
