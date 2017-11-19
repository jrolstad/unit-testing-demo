using System;
namespace myservice.mvc.test.TestUtility
{
    public class TestCompositionRoot
    {
        private TestCompositionRoot()
        {
        }

        public static TestCompositionRoot Create()
        {
            return new TestCompositionRoot();
        }

        public T Get<T>() where T: new()
        {
            return new T();
        }
    }
}
