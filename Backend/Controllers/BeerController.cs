using Backend.DTOs;
using Backend.Models;
using Backend.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerController : ControllerBase
    {
        private StoreContext _context;
        private IValidator<BeerInsertDto> _beerInsertValidator; //  Declaramos el validador
        private IValidator<BeerUpdateDto> _beerUpdateValidator;

        public BeerController(StoreContext context, IValidator<BeerInsertDto> beerInsertValidator, IValidator<BeerUpdateDto> beerUpdateValidator)  //  Recibimos el validador en los parametros del constructor
        {
            _context = context;
            _beerInsertValidator = beerInsertValidator; //  Asignamos el validador
            _beerUpdateValidator = beerUpdateValidator;
        }

        [HttpGet]
        public async Task<IEnumerable<BeerDto>> Get() => await _context.Beers.Select(e => new BeerDto { Id = e.BeerID, Name = e.Name, Alcohol = e.Alcohol, BrandID = e.BrandID}).ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<BeerDto>> GetById(int id)
        {
            var beer = await _context.Beers.FindAsync(id);

            if (beer == null)
            {
                return NotFound();
            }

            var beerDto = new BeerDto { Id = beer.BeerID, Name = beer.Name, BrandID = beer.BrandID, Alcohol = beer.Alcohol };

            return Ok(beerDto);
        }

        [HttpPost]
        public async Task<ActionResult<BeerDto>> Add(BeerInsertDto beerInsertDto)
        {
            var validationResult = await _beerInsertValidator.ValidateAsync(beerInsertDto); //  ValidateAsync(DtoOrClass) -> Ejecuta los validadores y determina si el DtoOrClass es valido o no

            if (!validationResult.IsValid)  //  Negamos la propidad is valid para que si el DtoOrClass no es valido (false) se ejecute la sentencia
            {
                return BadRequest(validationResult.Errors); //  Errors -> Contiene la lista de todos los errores del validador
            }

            var beer = new Beer()
            {
                Name = beerInsertDto.Name,
                BrandID = beerInsertDto.BrandID,
                Alcohol = beerInsertDto.Alcohol
            };

            await _context.Beers.AddAsync(beer);
            await _context.SaveChangesAsync();

            var beerDto = new BeerDto
            {
                Id = beer.BeerID,
                Name = beer.Name,
                Alcohol = beer.Alcohol,
                BrandID = beer.BrandID
            };

            return CreatedAtAction(nameof(GetById), new {id = beer.BeerID}, beerDto);   //  CreatedAtAction(route, parameters, inforBody) -> Permite retornar un response 'compelto' en el que devolveremos en los headers la url desde la cual se puede obtener el recurso retornado y adicionalmente podemos devolver inforamcion en el body
                                                                                                                //  nameof(method) -> Convierte a string el recurso especificado
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BeerDto>> Update(int id, BeerUpdateDto beerUpdateDto)
        {
            var validationResult = await _beerUpdateValidator.ValidateAsync(beerUpdateDto);
            var beer = await _context.Beers.FindAsync(id);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (beer == null)
            {
                return NotFound();
            }

            beer.Name = beerUpdateDto.Name;
            beer.Alcohol = beerUpdateDto.Alcohol;
            beer.BrandID = beerUpdateDto.BrandID;

            await _context.SaveChangesAsync();

            var beerDto = new BeerDto
            {
                Id = beer.BeerID,
                Name = beer.Name,
                Alcohol = beer.Alcohol,
                BrandID = beer.BrandID
            };

            return Ok(beerDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var beer = await _context.Beers.FindAsync(id);

            if (beer == null)
            {
                return NotFound();
            }

            _context.Beers.Remove(beer);

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
