using CurationAssistant.Models.SteemModels;
using CurationAssistant.Models.SteemModels.Partials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurationAssistant.Models
{
    public class BlogPostViewModel
    {
        public GetDiscussionModel Details { get; set; }
        public DiscussionJsonMetadata DiscussionMetadata { get; set; }
        public int WordCount { get; set; }
        public int ImageCount { get; set; }
    }
}