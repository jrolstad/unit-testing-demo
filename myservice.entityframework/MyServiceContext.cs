using Microsoft.EntityFrameworkCore;

namespace myservice.entityframework
{
    public class MyServiceContext:DbContext
    {
        public MyServiceContext(DbContextOptions<MyServiceContext> options) : base(options)
        {
            // Needed for migrations

            // Add-Migration -Name InitialDb -StartupProject myservice.EntityFramework -Project myservice.EntityFramework -Verbose

            // Update-Database -StartUpProject myservice.EntityFramework -Project myservice.EntityFramework -Verbose

            // Script-Migration -StartUpProject myservice.EntityFramework -Project myservice.EntityFramework -Verbose
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>(entity =>
            {
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(128);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(128);
                entity.Property(e => e.BirthDate).IsRequired();
            });
        }

        public DbSet<Person> Person { get; set; }

    }
}