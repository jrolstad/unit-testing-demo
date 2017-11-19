using System;
using Microsoft.Extensions.DependencyInjection;
using myservice.mvc.Config;

namespace myservice.mvc.test.TestUtility
{
    public class TestCompositionRoot
    {
        IServiceProvider _serviceProvider;

        TestCompositionRoot(IServiceCollection services)
        {
            _serviceProvider = services.BuildServiceProvider();
        }

        public static TestCompositionRoot Create()
        {
            var serviceCollection = new ServiceCollection();

            DependencyInjectionConfig.Configure(serviceCollection);

            return new TestCompositionRoot(serviceCollection);
        }

        public T Get<T>() where T : new()
        {
            return _serviceProvider.GetService<T>();
        }
    }
}
