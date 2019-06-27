using AutoMapper;
using CurationAssistant.Data.Models;
using CurationAssistant.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurationAssistant.Service.Mapper
{
    public static class AutoMapperConfiguration
    {
        public static void Configure(IMapperConfigurationExpression cfg)
        {            
            cfg.CreateMap<Block, BlockDTO>();
            cfg.CreateMap<Account, AccountDTO>();
        }
    }
}
