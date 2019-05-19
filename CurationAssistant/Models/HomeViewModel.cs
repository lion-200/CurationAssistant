using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurationAssistant.Models
{
    public class HomeViewModel
    {
        public string PostLink { get; set; }
        public ValidationVariables ValidationVariables { get; set; }

        public HomeViewModel()
        {
            ValidationVariables = new ValidationVariables();
        }

    }
}