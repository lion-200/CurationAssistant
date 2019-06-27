using System;

namespace CurationAssistant.Service.Models
{
    public class MemberDTO
    {
        public string community { get; set; }
        public string account { get; set; }
        public bool is_admin { get; set; }
        public bool is_mod { get; set; }
        public bool is_approved { get; set; }
        public bool is_muted { get; set; }
        public string Title { get; set; }
    }
}
