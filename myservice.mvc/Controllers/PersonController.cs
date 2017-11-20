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
        public OkObjectResult Get()
        {
            var data = _repository.Get();
            var result = _mapper.Map(data).ToList();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public OkObjectResult Get(int id)
        {
            var data = _repository.Get(id);
            var result = _mapper.Map(data);

            return Ok(result);
        }
    }
}