using System;

namespace CurationAssistant.Service.Models
{
    public class StateDTO
    {
        public int block_num { get; set; }
        public int db_version { get; set; }
        public decimal steem_per_mvest { get; set; }
        public decimal usd_per_steem { get; set; }
        public decimal sbd_per_steem { get; set; }
        public string dgpo { get; set; }
    }
}
