using myservice.entityframework;
using Microsoft.EntityFrameworkCore;

namespace myservice.mvc.test.TestUtility.EntityFramework
{
    public class InMemoryDbContext:MyServiceContext
    {
        private readonly string _databaseName;

        public InMemoryDbContext(string databaseName) : base(new DbContextOptions<MyServiceContext>())
        {
            _databaseName = databaseName;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseInMemoryDatabase(databaseName: _databaseName);
        }
    }
}