using Backend.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Services
{
    public interface ICommonService<T, TI, TU>  //  Implementación de generics en la interfaz
    {
        //  Dentro de los <> se ponen los generics que se van a utilizar en la interfaz. En este caso, se utiliza T para los retornos, TI para los DTO de inserción y TU para los DTO de actualizacion (puede ser con las letras que se quiera)
        Task<IEnumerable<T>> Get();
        //  Task<ActionResult<BeerDto>> GetById(int id);    Esto es un error, ya que la capa de servicio no es responsable de retornar ActionResult. Eso es parte de un response y es tarea del controller.
        Task<T> GetById(int id);    //  Esta es la forma correcta
        Task<T> Add(TI beerInsertDto);
        Task<T> Update(int id, TU beerUpdateDto);
        Task<T> Delete(int id);
    }
}
