using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurationAssistant.Data.Models
{
    [Table("hive_feed_cache")]
    public class FeedCache
    {
        public int post_id { get; set; }
        public int account_id { get; set; }
        public DateTime created_at { get; set; }
    }
}
