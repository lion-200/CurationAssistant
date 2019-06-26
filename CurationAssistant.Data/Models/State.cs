using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurationAssistant.Data.Models
{
    [Table("hive_state")]
    public class State
    {
        [Key]
        public int block_num { get; set; }
        public int db_version { get; set; }
        public decimal steem_per_mvest { get; set; }
        public decimal usd_per_steem { get; set; }
        public decimal sbd_per_steem { get; set; }
        public string dgpo { get; set; }
    }
}
