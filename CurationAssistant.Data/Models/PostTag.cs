using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurationAssistant.Data.Models
{
    [Table("hive_post_tags")]
    public class PostTag
    {
        public int post_id { get; set; }
        [StringLength(32)]
        public string tag { get; set; }
    }
}
