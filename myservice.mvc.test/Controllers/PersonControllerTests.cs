using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using myservice.mvc.Controllers;
using myservice.mvc.test.TestUtility;
using myservice.mvc.test.TestUtility.EntityFramework.Extensions;
using myservice.mvc.test.TestUtility.Extensions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace myservice.mvc.test.Controllers
{
    public class PersonControllerTests
    {
        [Fact]
        public void Get_NoSpecificRequest_ReturnsAllPersons()
        {
            // Arrange
            var root = TestCompositionRoot.Create();
            root.WithPerson(firstName: "the-test", lastName: "writer");
            root.WithPerson(firstName: "the-test", lastName: "reviewer");

            var controller = root.Get<PersonController>();

            // Act
            var response = controller.Get();

            // Assert
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response);

            var result = response.CastValue<ICollection<Application.Models.Person>>();
            Assert.Equal(2,result.Count);

            Assert.Equal("the-test", result.First().FirstName);
            Assert.Equal("writer", result.First().LastName);
            Assert.Equal("the-test writer", result.First().FullName);

            Assert.Equal("the-test", result.Last().FirstName);
            Assert.Equal("reviewer", result.Last().LastName);
            Assert.Equal("the-test reviewer", result.Last().FullName);
        }

        [Fact]
        public void Get_WithValidId_ReturnsMatchingPerson()
        {
            // Arrange
            var root = TestCompositionRoot.Create();
            root.WithPerson(firstName: "the-test", lastName: "writer");
            var person2 = root.WithPerson(firstName: "the-test", lastName: "reviewer");

            var controller = root.Get<PersonController>();

            // Act
            var response = controller.Get(person2.Id);

            // Assert
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response);

            var result = response.CastValue<Application.Models.Person>();

            Assert.Equal("the-test", result.FirstName);
            Assert.Equal("reviewer", result.LastName);
            Assert.Equal("the-test reviewer", result.FullName);
        }

        [Fact]
        public void Get_WithInValidId_ReturnsNotFound()
        {
            // Arrange
            var root = TestCompositionRoot.Create();
            root.WithPerson(firstName: "the-test", lastName: "writer");

            var controller = root.Get<PersonController>();

            // Act
            var response = controller.Get(250);

            // Assert
            Assert.NotNull(response);
            Assert.IsType<NotFoundResult>(response);
        }

        [Fact]
        public void Delete_WithValidId_DeletesMatchingPerson()
        {
            // Arrange
            var root = TestCompositionRoot.Create();
            root.WithPerson(firstName: "the-test", lastName: "writer");
            var person2 = root.WithPerson(firstName: "the-test", lastName: "reviewer");

            var controller = root.Get<PersonController>();

            // Act
            var response = controller.Delete(person2.Id);

            // Assert
            Assert.NotNull(response);
            Assert.IsType<OkResult>(response);

            var peopleResponse = controller.Get();
            var peopleResult = peopleResponse.CastValue<ICollection<Application.Models.Person>>();
            Assert.Equal(1, peopleResult.Count);
            Assert.Equal("writer", peopleResult.First().LastName);
        }

        [Fact]
        public void Delete_WithInValidId_ReturnsNotFound()
        {
            // Arrange
            var root = TestCompositionRoot.Create();
            root.WithPerson(firstName: "the-test", lastName: "writer");
            root.WithPerson(firstName: "the-test", lastName: "reviewer");

            var controller = root.Get<PersonController>();

            // Act
            var response = controller.Delete(250);

            // Assert
            Assert.NotNull(response);
            Assert.IsType<NotFoundResult>(response);

            var peopleResponse = controller.Get();
            var peopleResult = peopleResponse.CastValue<ICollection<Application.Models.Person>>();
            Assert.Equal(2, peopleResult.Count);
        }

        [Fact]
        public void Post_WithPersonData_CreatesNewPerson()
        {
            // Arrange
            var root = TestCompositionRoot.Create();
            root.WithPerson(firstName: "the-test", lastName: "writer");

            var controller = root.Get<PersonController>();
            var toPost = new Application.Models.Person
            {
                FirstName = "new-user",
                LastName = "created",
                BirthDate = DateTime.Today.AddDays(-1)
            };

            // Act
            var response = controller.Post(toPost);

            // Assert
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response);

            var peopleResponse = controller.Get();
            var result = peopleResponse.CastValue<ICollection<Application.Models.Person>>();

            Assert.Equal(2,result.Count);
        }
    }
}