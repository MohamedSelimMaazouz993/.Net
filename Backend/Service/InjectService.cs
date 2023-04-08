using Service.IService;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Service.Service;
using Core.Entities;
//using Service.ClientsServices;
using System.Diagnostics;
using System.Net.Http;

namespace Service
{
    public static class InjectService
    {
        public static IServiceCollection AddAllService(this IServiceCollection services)
        {

            var allProviderTypes = Assembly.GetAssembly(typeof(IUserService))
             .GetTypes().Where(t => t.Namespace != null).ToList();
            foreach (var intfc in allProviderTypes.Where(t => t.IsInterface))
            {
                var impl = allProviderTypes.FirstOrDefault(c => c.IsClass && intfc.Name.Substring(1) == c.Name);
                if (impl != null) services.AddTransient(intfc, impl);
            }
            
            
             
            return services;
        }
    }
}
