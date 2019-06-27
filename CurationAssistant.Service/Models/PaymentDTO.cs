using System;

namespace CurationAssistant.Service.Models
{
    public class PaymentDTO
    {
        public int id { get; set; }
        public int block_num { get; set; }
        public int tx_idx { get; set; }
        public int post_id { get; set; }
        public int from_account { get; set; }
        public int to_account { get; set; }        
        public decimal amount { get; set; }
        public string token { get; set; }
    }
}
