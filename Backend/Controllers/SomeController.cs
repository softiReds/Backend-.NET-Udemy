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

        [HttpGet("async")]
        public async Task<IActionResult> GetAsync()   //  async -> Especifica que el metodo será asíncrona; Task<Class> -> Representa una operación asincronica que puede incluir un resultado
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            stopwatch.Start();

            var task1 = new Task<int>(() => //  Task puede o no retornar algo. SI no retorna nada, no se ponen los signos <>
            {
                Thread.Sleep(1000);
                Console.WriteLine("Conexión a base de datos terminada");

                return 10;
            });

            var task2 = new Task<int>(() => 
            {
                Thread.Sleep(1000);
                Console.WriteLine("Conexión a base de datos terminada");

                return 20;
            });

            task1.Start();
            task2.Start();

            Console.WriteLine("Hago otra cosa");

            //  await task1;    //  await -> No continua la ejecución hasta que termine la ejecución del Task (en caso de no haber terminado antes)

            var resultTask1 = await task1;
            var resultTask2 = await task2;

            await Console.Out.WriteLineAsync("Todo ha terminado");

            stopwatch.Stop();

            return Ok($"{resultTask1} {resultTask2} {stopwatch.Elapsed}");
        }
    }
}
