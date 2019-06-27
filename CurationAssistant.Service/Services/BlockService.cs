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
    public class BlockService : IBlockService
    {
        private readonly HiveContext _hiveContext;
        private readonly IMapper _mapper;

        public BlockService(HiveContext hiveContext, IMapper mapper)
        {
            _hiveContext = hiveContext;
            _mapper = mapper;
        }

        public BlockDTO GetMostRecentBlock()
        {
            BlockDTO block = new BlockDTO();

            var entity = _hiveContext.Blocks.OrderByDescending(x => x.num).Take(1).FirstOrDefault();
            if(entity != null)
            {
                block = _mapper.Map<BlockDTO>(entity);
            }

            //using(var db = new HiveContext())
            //{

            //}

            return block;
        }
    }
}
