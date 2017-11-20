using System.Collections.Generic;
using System.Net;
using myservice.mvc.Controllers;
using myservice.mvc.test.TestUtility;
using myservice.mvc.test.TestUtility.Extensions;
using Xunit;

namespace myservice.mvc.test.Controllers
{
    public class PersonControllerTests
    {
        [Fact]
        public void Get_NoSpecificRequest_ReturnsValues()
        {
            // Arrange
            var root = TestCompositionRoot.Create();

            var controller = root.Get<PersonController>();

            // Act
            var response = controller.Get();

            // Assert
            Assert.NotNull(response);
            Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);

            var result = response.CastValue<ICollection<Application.Models.Person>>();
            Assert.NotEmpty(result);
        }
    }
}