using Backend.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Services
{
    public interface IBeerService
    {
        Task<IEnumerable<BeerDto>> Get();
        //  Task<ActionResult<BeerDto>> GetById(int id);    Esto es un error, ya que la capa de servicio no es responsable de retornar ActionResult. Eso es parte de un response y es tarea del controller.
        Task<BeerDto> GetById(int id);    //  Esta es la forma correcta
        Task<BeerDto> Add(BeerInsertDto beerInsertDto);
        Task<BeerDto> Update(int id, BeerUpdateDto beerUpdateDto);
        Task<BeerDto> Delete(int id);
    }
}
