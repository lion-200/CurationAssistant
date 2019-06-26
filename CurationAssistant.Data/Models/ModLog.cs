using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurationAssistant.Data.Models
{
    [Table("hive_modlog")]
    public class ModLog
    {
        [Key]
        public int id { get; set; }
        [StringLength(16)]
        public string community { get; set; }
        [StringLength(16)]
        public string account { get; set; }
        [StringLength(32)]
        public string action { get; set; }
        [StringLength(1000)]
        [Column("params")]
        public string parameters { get; set; }
        public DateTime created_at { get; set; }
    }
}
