using System.Linq;
using myservice.mvc.Application.Mappers;
using myservice.mvc.Application.Repositories;
using myservice.mvc.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace myservice.mvc.Controllers
{
    [Route("api/[controller]")]
    public class PersonController : Controller
    {
        private readonly PersonRepository _repository;
        private readonly PersonMapper _mapper;
        private readonly IIdentityService _identityService;

        public PersonController(PersonRepository repository, PersonMapper mapper, IIdentityService identityService)
        {
            _repository = repository;
            _mapper = mapper;
            _identityService = identityService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var data = _repository.Get();

            var aliases = data.Select(d => d.Alias);
            var userData = _identityService.GetUsers(aliases);

            var result = _mapper.Map(data,userData.EmailAddresses).ToList();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var data = _repository.Get(id);
            if (data == null)
                return NotFound();

            var userData = _identityService.GetUsers(new[]{data.Alias});
            var result = _mapper.Map(data,userData.EmailAddresses);

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post(Application.Models.Person toPost)
        {
            var dataModel = _mapper.Map(toPost);
            var result = _repository.Create(dataModel);

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