using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurationAssistant.Models.SteemModels
{
    public class FindVotesModel
    {
        public List<FindVotesItemModel> Items { get; set; }
        public FindVotesModel()
        {
            Items = new List<FindVotesItemModel>();
        }
    }
}