using System.Collections.Generic;
using myservice.mvc.test.TestUtility;
using myservice.mvc.Controllers;
using Xunit;
using System.Net;
using myservice.mvc.test.TestUtility.Extensions;

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

            var result = response.CastValue<ICollection<string>>();
            Assert.Equal(new[]{"value1","value2"},result);
        }

        [Fact]
        public void Get_WithId_ReturnsSpecificValue()
        {
            // Arrange
            var root = TestCompositionRoot.Create();

            var controller = root.Get<ValuesController>();

            // Act
            var response = controller.Get(42);

            // Assert
            Assert.NotNull(response);
            Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);

            var result = response.CastValue<string>();
            Assert.Equal("value", result);
        }
    }
}
