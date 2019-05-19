using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurationAssistant.Models.SteemModels.Partials
{
    public class TransactionModel
    {
        public string trx_id { get; set; }
        public ulong block { get; set; }
        public ulong trx_in_block { get; set; }
        public ulong op_in_trx { get; set; }
        public int virtual_op { get; set; }
        public DateTime timestamp { get; set; }
        public List<object> op { get; set; }
    }
}