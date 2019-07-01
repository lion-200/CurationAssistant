using CurationAssistant.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurationAssistant.Service.ServiceInterfaces
{
    public interface IPostService
    {
        List<PostCacheDTO> GetMostRecentPostsContainingVoter(string author, string voter, int postCount);
    }
}
