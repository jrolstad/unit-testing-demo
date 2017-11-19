using Microsoft.AspNetCore.Mvc;

namespace myservice.mvc.test.TestUtility.Extensions
{
    public static class HttpExtensions
    {
        public static T CastValue<T>(this ObjectResult result)
        {
            return (T)result.Value;
        }
    }
}
