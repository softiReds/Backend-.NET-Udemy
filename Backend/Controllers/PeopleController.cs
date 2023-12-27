using Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private IPeopleService _peopleService;

        public PeopleController([FromKeyedServices("peopleService")] IPeopleService peopleService)  //  [FromKeyedServices("key") -> Permite especificar la llave de la dependencia a la que se quiere acceder
        {
            _peopleService = peopleService;
        }

        [HttpGet("all")]
        public List<People> GetPeople() => Repository.People;   //  Se retorna directamente el atributo estático de la clase Repository

        [HttpGet("{id}")]   //  Especificación de parametros recibidos desde la URL (para agregar más -> {e}/{e})
        public ActionResult<People> Get(int id)
        {
            var people = Repository.People.FirstOrDefault(e => e.Id == id);

            if (people == null) return NotFound();

            return Ok(people);
        }

        [HttpGet("search/{search}")]
        public List<People> Get(string search) =>
            Repository.People.Where(e => e.Name.ToUpper().Contains(search.ToUpper())).ToList(); //  Contains(string) -> Funciona como el LIKE en SQL, busca que el string evaluado contenga el string enviado en el parametro

        [HttpPost]
        public IActionResult Add(People people) //  IActionResult -> Retorna unicamente el status code (usado cuando no se requiere retornar INFORMACION en el Request Body)
        {
            if (!_peopleService.Validate(people)) return BadRequest();

            Repository.People.Add(people);

            return NoContent();
        }
    }

    public class Repository
    {
        public static List<People> People = new List<People>
        {
            new People() { Id = 1, Name = "Santiago", BirthDate = new DateTime(2005, 10, 14) },
            new People() { Id = 2, Name = "Pedro", BirthDate = new DateTime(1992, 11, 3) },
            new People() { Id = 3, Name = "Anna", BirthDate = new DateTime(1985, 1, 8) }
        };
    }

    public class People
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
