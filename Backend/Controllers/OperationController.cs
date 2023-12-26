using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationController : ControllerBase
    {
        [HttpGet]
        public decimal Add(decimal a, decimal b)
        {
            return a + b;
        }

        [HttpPost]
        public decimal Add(Numbers numbers, [FromHeader] string Host, [FromHeader(Name = "Content-Length")] string ContentLength)   //  [FromHeader(Name = "name")] -> Permite que accedamos a información del header, el nombre del parametro debe ser identico al nombre de la propiedad en el header a la que queremos acceder
        {
            /*
             * 
             *  Para agregar un header personalizado se utiliza la siguiente nomenclatura: X-NombreHeaderPersonalizado. Esto se hace desde PostMan y para acceder al valor de ese header debemos hacer uso de la propiedad name en [FromHeader]
             * 
             */

            Console.WriteLine(Host);
            Console.WriteLine(ContentLength);
            return numbers.A - numbers.B;
        }

        [HttpPut]
        public decimal Edit(decimal a, decimal b)
        {
            return a * b;
        }

        [HttpDelete]
        public decimal Delete(decimal a, decimal b)
        {
            return a / b;
        }
    }

    public class Numbers
    {
        public decimal A { get; set; }
        public decimal B { get; set; }
    }
}
