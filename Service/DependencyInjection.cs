using AutoMapper.Extensions.ExpressionMapping;
using DAL.Injections;
using Service.Common.Mappings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.IService;
using Service.Service;

namespace Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(cfg=> { cfg.AddExpressionMapping(); },typeof(MappingProfile).Assembly);
            
            services.InjectPersistence();
            services.AddAllService();
            

            return services;
        }
    }
}
