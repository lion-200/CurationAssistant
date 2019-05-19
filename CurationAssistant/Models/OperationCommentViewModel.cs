using CurationAssistant.Models.SteemModels.Partials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurationAssistant.Models
{
    public class OperationCommentViewModel : BaseTransactionOperationModel
    {
        public string parent_author { get; set; }
        public string parent_permlink { get; set; }
        public string title { get; set; }
        public string body { get; set; }
        public string json_metadata { get; set; }
    }
}