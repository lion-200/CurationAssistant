using CurationAssistant.Models.SteemModels.Partials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurationAssistant.Models
{
    public class ActionViewModel
    {
        public string Type { get; set; }
        public DateTime TimeStamp { get; set; }
        public BaseTransactionOperationModel Details { get; set; }
    }
}