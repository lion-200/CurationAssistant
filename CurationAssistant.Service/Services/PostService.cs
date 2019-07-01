using AutoMapper;
using CurationAssistant.Data;
using CurationAssistant.Service.Models;
using CurationAssistant.Service.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurationAssistant.Service.Services
{
    public class PostService : IPostService
    {
        private readonly HiveContext _hiveContext;
        private readonly IMapper _mapper;

        public PostService(HiveContext hiveContext, IMapper mapper)
        {
            _hiveContext = hiveContext;
            _mapper = mapper;
        }

        public List<PostCacheDTO> GetMostRecentPostsContainingVoter(string author, string voter, int postCount)
        {
            List<PostCacheDTO> response = _hiveContext.PostCache.Where(x => x.author == author && x.votes.Contains(voter))
                                                        .Select(x => new PostCacheDTO
                                                        {
                                                            author = x.author,
                                                            title = x.title,
                                                            permlink = x.permlink,
                                                            votes = x.votes,
                                                            created_at = x.created_at
                                                        }).OrderByDescending(x => x.created_at).Take(postCount).ToList();

            return response;
        }
    }
}
