using System.Collections;
using System.Collections.Generic;
using System.Linq;
using myservice.entityframework;


namespace myservice.mvc.Application.Mappers
{
    public class PersonMapper
    {
        public IEnumerable<Models.Person> Map(IEnumerable<myservice.entityframework.Person> toMap, Dictionary<string,string> emailAddresses)
        {
            return toMap
                .Select(d=>Map(d,emailAddresses));
        }

        public Models.Person Map(entityframework.Person toMap, Dictionary<string, string> emailAddresses)
        {
            var address = ResolveEmailAddress(toMap, emailAddresses);

            return new Models.Person
            {
                Id = toMap.Id,
                Alias = toMap.Alias,
                EmailAddress = address,
                FirstName = toMap.FirstName,
                LastName = toMap.LastName,
                FullName = $"{toMap.FirstName} {toMap.LastName}",
                BirthDate = toMap.BirthDate
            };
        }

        private static string ResolveEmailAddress(Person toMap, Dictionary<string, string> emailAddresses)
        {
            if (string.IsNullOrWhiteSpace(toMap.Alias))
            {
                return null;
            }
            else
            {
                emailAddresses.TryGetValue(toMap.Alias, out var address);

                return address;
            }
           
        }

        public entityframework.Person Map(Models.Person toMap)
        {
            return new entityframework.Person
            {
                Id = toMap.Id,
                Alias = toMap.Alias,
                FirstName = toMap.FirstName,
                LastName = toMap.LastName,
                BirthDate = toMap.BirthDate
            };
        }
    }
}