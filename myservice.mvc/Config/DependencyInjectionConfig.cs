using System;
using Microsoft.Extensions.DependencyInjection;
using myservice.mvc.Controllers;

namespace myservice.mvc.Config
{
    public class DependencyInjectionConfig
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddTransient<ValuesController>();
        }
    }
}
