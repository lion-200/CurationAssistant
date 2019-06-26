using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurationAssistant.Data.Models
{
    [Table("hive_reblogs")]
    public class ReBlog
    {
        [StringLength(16)]
        public string account { get; set; }
        public int post_id { get; set; }
        public DateTime created_at { get; set; }
    }
}
