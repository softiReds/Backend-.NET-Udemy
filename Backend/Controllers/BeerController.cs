﻿using Backend.DTOs;
using Backend.Models;
using Backend.Services;
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
        //private StoreContext _context;
        private IValidator<BeerInsertDto> _beerInsertValidator; //  Declaramos el validador
        private IValidator<BeerUpdateDto> _beerUpdateValidator;
        private ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto> _beerService; //  Se envían los datos con los que trabajará la interfaz

        public BeerController(/*StoreContext context, */IValidator<BeerInsertDto> beerInsertValidator, IValidator<BeerUpdateDto> beerUpdateValidator, [FromKeyedServices("beerService")]ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto> beerService)  //  Recibimos el validador en los parametros del constructor
        {
            //_context = context;
            _beerInsertValidator = beerInsertValidator; //  Asignamos el validador
            _beerUpdateValidator = beerUpdateValidator;
            _beerService = beerService;
        }

        [HttpGet]
        public async Task<IEnumerable<BeerDto>> Get() => await _beerService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<BeerDto>> GetById(int id)
        {
            var beerDto = await _beerService.GetById(id);

            return beerDto == null ? NotFound() : Ok(beerDto);  //  If ternario
        }

        [HttpPost]
        public async Task<ActionResult<BeerDto>> Add(BeerInsertDto beerInsertDto)
        {
            //  Las validaciones de formato se dejan en el controller

            var validationResult = await _beerInsertValidator.ValidateAsync(beerInsertDto); //  ValidateAsync(DtoOrClass) -> Ejecuta los validadores y determina si el DtoOrClass es valido o no

            if (!validationResult.IsValid)  //  Negamos la propidad is valid para que si el DtoOrClass no es valido (false) se ejecute la sentencia
            {
                return BadRequest(validationResult.Errors); //  Errors -> Contiene la lista de todos los errores del validador
            }

            var beerDto = await _beerService.Add(beerInsertDto);

            return CreatedAtAction(nameof(GetById), new {id = beerDto.Id}, beerDto);   //  CreatedAtAction(route, parameters, inforBody) -> Permite retornar un response 'compelto' en el que devolveremos en los headers la url desde la cual se puede obtener el recurso retornado y adicionalmente podemos devolver inforamcion en el body
                                                                                                                //  nameof(method) -> Convierte a string el recurso especificado
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BeerDto>> Update(int id, BeerUpdateDto beerUpdateDto)
        {
            var validationResult = await _beerUpdateValidator.ValidateAsync(beerUpdateDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var beerDto = await _beerService.Update(id, beerUpdateDto);

            return beerDto == null ? NotFound() : Ok(beerDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BeerDto>> Delete(int id)
        {
            var beerDto = await _beerService.Delete(id);

            return beerDto == null ? NotFound() : Ok(beerDto);
        }
    }
}
