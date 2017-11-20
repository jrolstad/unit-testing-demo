using Microsoft.AspNetCore.Mvc;

namespace myservice.mvc.test.TestUtility.Extensions
{
    public static class MvcExtensions
    {
        public static T CastValue<T>(this ObjectResult result)
        {
            return (T)result.Value;
        }
    }
}
