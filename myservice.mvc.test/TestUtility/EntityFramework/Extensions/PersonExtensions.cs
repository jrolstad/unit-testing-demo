using System;
using System.Collections.Generic;
using System.Linq;
using myservice.entityframework;

namespace myservice.mvc.test.TestUtility.EntityFramework.Extensions
{
    public static class PersonExtensions
    {
        public static entityframework.Person WithPerson(this TestCompositionRoot root, 
            string firstName = "my-first-name", 
            string lastName = "my-last-name", 
            DateTime? birthDate = null)
        {
            var context = root.Get<MyServiceContext>();

            var person = new Person
            {
                FirstName = firstName,
                LastName = lastName,
                BirthDate = birthDate ?? DateTime.Now.AddYears(-25)
            };

            context.Person.Add(person);
            context.SaveChanges();

            return person;
        }
    }
}