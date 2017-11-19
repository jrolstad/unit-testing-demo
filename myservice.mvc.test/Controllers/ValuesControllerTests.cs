using System;
using myservice.mvc.test.TestUtility;
using myservice.mvc.Controllers;
using Xunit;
using System.Net;

namespace myservice.mvc.test.Controllers
{
    public class ValuesControllerTests
    {
        [Fact]
        public void Get_NoSpecificRequest_ReturnsValues()
        {
            // Arrange
            var root = TestCompositionRoot.Create();

            var controller = root.Get<ValuesController>();

            // Act
            var response = controller.Get();

            // Assert
            Assert.NotNull(response);
            Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);
        }
    }
}
