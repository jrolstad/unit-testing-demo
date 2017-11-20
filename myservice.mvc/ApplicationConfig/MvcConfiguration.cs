using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace myservice.mvc.ApplicationConfig
{
    public static class MvcConfiguration
    {
        public static void Register(IServiceCollection services)
        {
            services.AddMvc();
        }

        public static void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }
    }
}