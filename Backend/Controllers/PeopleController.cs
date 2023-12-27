using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        [HttpGet("all")]
        public List<People> GetPeople() => Repository.People;
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
