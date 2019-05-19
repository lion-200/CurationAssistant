using CurationAssistant.Models.SteemModels.Partials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurationAssistant.Models
{
    public class OperationVoteViewModel : BaseTransactionOperationModel
    {
        public string voter { get; set; }
        public int weight { get; set; }
    }
}