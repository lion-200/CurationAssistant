using CurationAssistant.Models.SteemModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurationAssistant.Models.Becquerel
{
    public class BqResponseModel
    {
        public BqPostModel post { get; set; }
        public int status { get; set; }
    }
}