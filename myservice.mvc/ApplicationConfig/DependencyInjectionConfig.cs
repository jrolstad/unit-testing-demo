using myservice.entityframework;
using myservice.mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace myservice.mvc.ApplicationConfig
{
    public class DependencyInjectionConfig
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ValuesController>();

            services.AddDbContext<MyServiceContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("MyServiceDatabase");

                options.UseSqlServer(connectionString);
            });
        }
    }
}
