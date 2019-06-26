using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurationAssistant.Data.Models
{
    [Table("hive_accounts")]
    public class Account
    {
        [Key]
        public int id { get; set; }
        [StringLength(16)]
        public string name { get; set; }
        public DateTime created_at { get; set; }
        public Single reputation { get; set; }
        [StringLength(20)]
        public string display_name { get; set; }
        [StringLength(160)]
        public string about { get; set; }
        [StringLength(30)]
        public string location { get; set; }
        [StringLength(100)]
        public string website { get; set; }
        [StringLength(1024)]
        public string profile_image { get; set; }
        [StringLength(1024)]
        public string cover_image { get; set; }
        public int followers { get; set; }
        public int following { get; set; }
        [StringLength(16)]
        public string proxy { get; set; }
        public int post_count { get; set; }
        public Single proxy_weight { get; set; }
        public Single vote_weight { get; set; }
        public int kb_used { get; set; }
        public int rank { get; set; }
        public DateTime active_at { get; set; }
        public DateTime cached_at { get; set; }
        public string raw_json { get; set; }
    }
}
