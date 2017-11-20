using System.Linq;
using myservice.mvc.Application.Mappers;
using myservice.mvc.Application.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace myservice.mvc.Controllers
{
    [Route("api/[controller]")]
    public class PersonController : Controller
    {
        private readonly PersonRepository _repository;
        private readonly PersonMapper _mapper;

        public PersonController(PersonRepository repository, PersonMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var data = _repository.Get();
            var result = _mapper.Map(data).ToList();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var data = _repository.Get(id);
            if (data == null)
                return NotFound();

            var result = _mapper.Map(data);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var person = _repository.Get(id);
            if (person == null)
                return NotFound();

            _repository.Delete(person);

            return Ok();
        }
    }
}