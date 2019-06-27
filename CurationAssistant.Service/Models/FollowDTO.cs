using System;

namespace CurationAssistant.Service.Models
{
    public class FollowDTO
    {
        public int follower { get; set; }
        public int following { get; set; }
        public int state { get; set; }
        public DateTime created_at { get; set; }
    }
}
