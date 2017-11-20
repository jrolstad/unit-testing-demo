using System;
using Microsoft.Extensions.DependencyInjection;
using myservice.mvc.Config;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.Extensions.Configuration;
using Moq;

namespace myservice.mvc.test.TestUtility
{
    public class TestCompositionRoot
    {
        private readonly TestContext _testContext;
        private readonly IServiceCollection _serviceCollection;
        private IServiceProvider _serviceProvider = null;

        private TestCompositionRoot(IServiceCollection services, TestContext testContext)
        {
            _testContext = testContext;
            _serviceCollection = services;
        }

        public static TestCompositionRoot Create()
        {
            var serviceCollection = new ServiceCollection();
            var context = new TestContext();

            return new TestCompositionRoot(serviceCollection, context);
        }

        public T Get<T>() where T : new()
        {
            if (_serviceProvider == null)
            {
                _serviceProvider = Build();
            }

            return _serviceProvider.GetService<T>();
        }

        private IServiceProvider Build()
        {
            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(_testContext.ConfigurationValues)
                .Build();

            var startupInstance = new Startup(config);
            startupInstance.ConfigureServices(_serviceCollection);


            return _serviceCollection.BuildServiceProvider();
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
