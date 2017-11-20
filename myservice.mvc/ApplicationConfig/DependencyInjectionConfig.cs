using myservice.entityframework;
using myservice.mvc.Application.Mappers;
using myservice.mvc.Application.Repositories;
using myservice.mvc.Application.Services;
using myservice.mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Person = myservice.mvc.Application.Models.Person;

namespace myservice.mvc.ApplicationConfig
{
    public class DependencyInjectionConfig
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ValuesController>();
            services.AddTransient<PersonController>();

            services.AddTransient<PersonRepository>();
            services.AddTransient<PersonMapper>();
            services.AddTransient<IIdentityService, IdentityService>();

            services.AddDbContext<MyServiceContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("MyServiceDatabase");

                options.UseSqlServer(connectionString);
            });
        }
    }
}
