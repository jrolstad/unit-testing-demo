using System.Collections;
using System.Collections.Generic;
using System.Linq;
using myservice.entityframework;
using Microsoft.EntityFrameworkCore;

namespace myservice.mvc.Application.Repositories
{
    public class PersonRepository
    {
        private readonly MyServiceContext _context;

        public PersonRepository(MyServiceContext context)
        {
            _context = context;
        }

        public ICollection<entityframework.Person> Get()
        {
            return _context
                .Person
                .AsNoTracking()
                .ToList();
        }

        public entityframework.Person Get(int id)
        {
            return _context
                .Person
                .Find(id);
        }

        public void Delete(Person toDelete)
        {
            _context.Person.Remove(toDelete);
            _context.SaveChanges();
        }
    }
}