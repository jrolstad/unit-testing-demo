using System;
using myservice.entityframework;
using myservice.mvc.Application.Services;
using myservice.mvc.test.TestUtility.EntityFramework;
using myservice.mvc.test.TestUtility.Extensions;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;

namespace myservice.mvc.test.TestUtility
{
    public class TestCompositionRoot
    {
        public readonly TestContext TestContext;
        private readonly IServiceCollection _serviceCollection;
        private IServiceProvider _serviceProvider = null;

        private TestCompositionRoot(IServiceCollection services, TestContext testContext)
        {
            TestContext = testContext;
            _serviceCollection = services;
        }

        public static TestCompositionRoot Create()
        {
            var serviceCollection = new ServiceCollection();
            var context = new TestContext();

            return new TestCompositionRoot(serviceCollection, context);
        }

        public T Get<T>()
        {
            if (_serviceProvider == null)
            {
                _serviceProvider = Build();
            }

            var instance = _serviceProvider.GetService<T>();

            if (instance == null)
                throw new ArgumentOutOfRangeException($"Unable to resolve instance of type {typeof(T).FullName}");

            ConfigureIfController(instance);

            return instance;
        }

        private static void ConfigureIfController<T>(T instance)
        {
            if (instance is Controller)
            {
                (instance as Controller).WithHttpConfiguration();
            }
        }

        private IServiceProvider Build()
        {
            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(TestContext.ConfigurationValues)
                .Build();

            var hostingEnvironment = new HostingEnvironment
            {
                EnvironmentName = "Testing",
                ContentRootPath = Environment.CurrentDirectory
            };

            var startupInstance = new Startup(hostingEnvironment) { Configuration = config };
            startupInstance.ConfigureServices(_serviceCollection);

            RegisterInMemoryDbContext(_serviceCollection);
            var identityService = RegisterMock<IIdentityService>(_serviceCollection);
            identityService.Configure(TestContext);

            return _serviceCollection.BuildServiceProvider();
        }

        private void RegisterInMemoryDbContext(IServiceCollection services)
        {
            var dbContext = new InMemoryDbContext(TestContext.InMemoryDatabaseIdentitifer);
            services.Replace(new ServiceDescriptor(typeof(MyServiceContext), dbContext));
        }

        private Mock<T> RegisterMock<T>(IServiceCollection services) where T : class
        {
            var mock = new Mock<T>();

            services.AddSingleton(mock);

            services.AddSingleton(mock.Object);

            return mock;
        }
    }
}
