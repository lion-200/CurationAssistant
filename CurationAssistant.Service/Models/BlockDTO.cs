using System;

namespace CurationAssistant.Service.Models
{
    public class BlockDTO
    {
        public int num { get; set; }
        public string hash { get; set; }
        public string prev { get; set; }
        public int txs { get; set; }
        public int ops { get; set; }
        public DateTime created_at { get; set; }
    }
}
