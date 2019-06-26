using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurationAssistant.Data.Models
{
    [Table("hive_members")]
    public class Member
    {
        [StringLength(16)]
        public string community { get; set; }
        [StringLength(16)]
        public string account { get; set; }
        public bool is_admin { get; set; }
        public bool is_mod { get; set; }
        public bool is_approved { get; set; }
        public bool is_muted { get; set; }
        [StringLength(255)]
        public string Title { get; set; }
    }
}
