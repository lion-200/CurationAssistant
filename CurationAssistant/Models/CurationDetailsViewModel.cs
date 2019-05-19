using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurationAssistant.Models
{
    public class CurationDetailsViewModel
    {
        public AuthorViewModel Author { get; set; }
        public BlogPostViewModel BlogPost { get; set; }

        public List<ActionViewModel> Comments { get; set; }
        public List<ActionViewModel> Posts { get; set; }
        public List<ActionViewModel> Votes { get; set; }

        public DateTime LastTransactionDate { get; set; }

        public CurationDetailsViewModel()
        {
            Author = new AuthorViewModel();
            BlogPost = new BlogPostViewModel();
            Comments = new List<ActionViewModel>();
            Posts = new List<ActionViewModel>();
            Votes = new List<ActionViewModel>();
        }
    }
}