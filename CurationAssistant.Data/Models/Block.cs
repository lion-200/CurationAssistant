using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurationAssistant.Data.Models
{
    [Table("hive_blocks")]
    public class Block
    {
        [Key]
        public int num { get; set; }
        [StringLength(40)]
        public string hash { get; set; }
        [StringLength(40)]
        public string prev { get; set; }
        public int txs { get; set; }
        public int ops { get; set; }
        public DateTime created_at { get; set; }
    }
}
