using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SomeController : ControllerBase
    {
        [HttpGet("sync")]
        public IActionResult GetSync()
        {
            Stopwatch stopwatch = Stopwatch.StartNew(); //  Stopwatch -> Simula un cronometro
            stopwatch.Start();  //  Start() -> Inicia el cronometro

            Thread.Sleep(1000); //  Sleep(int seconds) -> Detiene el sistema durante los segundos especificados
            Console.WriteLine("Conexión a base de datos terminada");

            Thread.Sleep(1000);
            Console.WriteLine("Envío de mail terminado");

            Console.WriteLine("Todo ha terminado");

            stopwatch.Stop();   //  Stop() -> Detiene el cronometro

            return Ok(stopwatch.Elapsed);   // Elapsed -> Obtiene el tiempo total del cronometro
        }
    }
}
