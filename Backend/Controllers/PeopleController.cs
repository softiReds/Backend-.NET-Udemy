using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        [HttpGet("all")]
        public List<People> GetPeople() => Repository.People;   //  Se retorna directamente el atributo estático de la clase Repository

        [HttpGet("{id}")]   //  Especificación de parametros recibidos desde la URL (para agregar más -> {e}/{e})
        public People Get(int id) => Repository.People.First(e => e.Id == id);  //  First(e => e.Property == Parameter) -> Recorre la lista y hace un filtro para retornar el primer elemento que cumpla con la condición

        [HttpGet("search/{search}")]
        public List<People> Get(string search) =>
            Repository.People.Where(e => e.Name.ToUpper().Contains(search.ToUpper())).ToList(); //  Contains(string) -> Funciona como el LIKE en SQL, busca que el string evaluado contenga el string enviado en el parametro
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
