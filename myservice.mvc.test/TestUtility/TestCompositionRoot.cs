using System;
using myservice.mvc.test.TestUtility.Extensions;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
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

            var instance = _serviceProvider.GetService<T>();
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
                .AddInMemoryCollection(_testContext.ConfigurationValues)
                .Build();

            var hostingEnvironment = new HostingEnvironment
            {
                EnvironmentName = "Testing",
                ContentRootPath = Environment.CurrentDirectory
            };

            var startupInstance = new Startup(hostingEnvironment) { Configuration = config };
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
