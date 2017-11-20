using Microsoft.AspNetCore.Mvc;

namespace myservice.mvc.test.TestUtility.Extensions
{
    public static class MvcExtensions
    {
        public static T CastValue<T>(this IActionResult response)
        {
            var objectContent = (ObjectResult)response;

            return (T)objectContent.Value;
        }
    }
}
