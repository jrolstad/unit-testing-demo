using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace myservice.mvc.Application.Mappers
{
    public class PersonMapper
    {
        public IEnumerable<Models.Person> Map(IEnumerable<myservice.entityframework.Person> toMap)
        {
            return toMap
                .Select(Map);
        }

        public Models.Person Map(entityframework.Person toMap)
        {
            return new Models.Person
            {
                Id = toMap.Id,
                FirstName = toMap.FirstName,
                LastName = toMap.LastName,
                FullName = $"{toMap.FirstName} {toMap.LastName}",
                BirthDate = toMap.BirthDate
            };
        }

        public entityframework.Person Map(Models.Person toMap)
        {
            return new entityframework.Person
            {
                Id = toMap.Id,
                FirstName = toMap.FirstName,
                LastName = toMap.LastName,
                BirthDate = toMap.BirthDate
            };
        }
    }
}