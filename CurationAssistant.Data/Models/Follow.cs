using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurationAssistant.Data.Models
{
    [Table("hive_follows")]
    public class Follow
    {
        public int follower { get; set; }
        public int following { get; set; }
        public int state { get; set; }
        public DateTime created_at { get; set; }
    }
}
