using AutoMapper;
using CurationAssistant.Data;
using CurationAssistant.Service.Mapper;
using CurationAssistant.Service.ServiceInterfaces;
using CurationAssistant.Service.Services;
using System.Web.Mvc;
using Unity;
using Unity.Lifetime;
using Unity.Mvc5;

namespace CurationAssistant
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<HiveContext>(new HierarchicalLifetimeManager());

            container.RegisterType<IAccountService, AccountService>();
            container.RegisterType<IBlockService, BlockService>();
            container.RegisterType<ICommunityService, CommunityService>();
            container.RegisterType<IFeedCacheService, FeedCacheService>();
            container.RegisterType<IFlagService, FlagService>();
            container.RegisterType<IFollowService, FollowService>();
            container.RegisterType<IModLogService, ModLogService>();
            container.RegisterType<IPaymentService, PaymentService>();
            container.RegisterType<IPostService, PostService>();
            container.RegisterType<IStateService, StateService>();            

            // AutoMapper start
            var config = new MapperConfiguration(cfg =>
            {
                AutoMapperConfiguration.Configure(cfg);
            });

            IMapper mapper = config.CreateMapper();
            container.RegisterInstance(mapper);
            // AutoMapper end

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}