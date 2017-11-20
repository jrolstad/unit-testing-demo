using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace myservice.mvc.test.TestUtility.Extensions
{
    public static class ControllerExtensions
    {
        public static T WithHttpConfiguration<T>(this T controller) where T : Controller
        {

            if (controller.HttpContext == null)
            {
                controller.ControllerContext.HttpContext = new DefaultHttpContext();
            }

            return controller;
        }
    }
}