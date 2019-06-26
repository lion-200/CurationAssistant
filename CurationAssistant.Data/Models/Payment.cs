using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurationAssistant.Data.Models
{
    [Table("hive_payments")]
    public class Payment
    {
        [Key]
        public int id { get; set; }
        public int block_num { get; set; }
        public int tx_idx { get; set; }
        public int post_id { get; set; }
        public int from_account { get; set; }
        public int to_account { get; set; }        
        public decimal amount { get; set; }
        [StringLength(5)]
        public string token { get; set; }
    }
}
