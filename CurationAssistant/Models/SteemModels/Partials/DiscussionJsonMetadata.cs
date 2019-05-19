using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurationAssistant.Models.SteemModels.Partials
{
    public class DiscussionJsonMetadata
    {
        public List<string> tags { get; set; }
        public List<string> users { get; set; }
        public List<string> image { get; set; }
        public List<string> links { get; set; }
        public string app { get; set; }
        public string format { get; set; }
    }
}