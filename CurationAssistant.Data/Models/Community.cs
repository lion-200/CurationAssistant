using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurationAssistant.Data.Models
{
    [Table("hive_communities")]
    public class Community
    {
        [Key]
        [StringLength(16)]
        public string name { get; set; }
        [StringLength(32)]
        public string title { get; set; }
        [StringLength(255)]
        public string about { get; set; }
        [StringLength(5000)]
        public string description { get; set; }
        [StringLength(2)]
        public string lang { get; set; }
        public string settings { get; set; }
        public int type_id { get; set; }
        public bool is_nsfw { get; set; }
        public DateTime created_at { get; set; }
    }
}
