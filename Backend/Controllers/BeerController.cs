using Backend.DTOs;
using Backend.Models;
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

        public BeerController(StoreContext context)
        {
            _context = context;
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
    }
}
