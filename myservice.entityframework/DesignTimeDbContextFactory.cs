using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace myservice.entityframework
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MyServiceContext>
    {
        public MyServiceContext CreateDbContext(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true);

            configurationBuilder.AddJsonFile($"C:\\machineSettings.json", optional: true);

            var configuration = configurationBuilder.Build();

            var builder = new DbContextOptionsBuilder<MyServiceContext>();

            var connectionString = configuration.GetConnectionString("MyServiceDatabase");

            builder.UseSqlServer(connectionString);

            return new MyServiceContext(builder.Options);
        }
    }
}