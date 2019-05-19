using CurationAssistant.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurationAssistant.Models
{
    public class ValidationItemViewModel
    {
        public string Title { get; set; }
        public ValidationPriority Priority { get; set; }
        public string PriorityDescription { get; set; }
        public ValidationResultType ResultType { get; set; }
        public string ResultTypeDescription { get; set; }
        public string ResultMessage { get; set; }
        public int OrderId { get; set; }
    }
}