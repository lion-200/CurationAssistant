using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurationAssistant.Data.Models
{
    [Table("hive_posts")]
    public class Post
    {
        [Key]
        public int id { get; set; }
        public int parent_id { get; set; }
        [StringLength(16)]
        public string author { get; set; }
        [StringLength(255)]
        public string permlink { get; set; }
        [StringLength(16)]
        public string community { get; set; }
        [StringLength(255)]
        public string category { get; set; }
        public int depth { get; set; }
        public DateTime created_at { get; set; }
        public bool is_deleted { get; set; }
        public bool is_muted { get; set; }
        public bool is_valid { get; set; }
        public bool is_pinned { get; set; }
        public decimal promoted { get; set; }
    }
}
