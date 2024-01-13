using Backend.DTOs;
using Backend.Models;
using Backend.Repository;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class BeerService : ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto>
    {
        private StoreContext _context;
        private IRepository<Beer> _beerRepository;

        public BeerService(StoreContext context, IRepository<Beer> beerRepository) 
        {
            _context = context;
            _beerRepository = beerRepository;
        }

        public async Task<IEnumerable<BeerDto>> Get()
        {
            //  await _context.Beers.Select(e => new BeerDto { Id = e.BeerID, Name = e.Name, Alcohol = e.Alcohol, BrandID = e.BrandID }).ToListAsync();

            var beers = await _beerRepository.Get();

            return beers.Select(e => new BeerDto { Id = e.BeerID, Name = e.Name, Alcohol = e.Alcohol, BrandID = e.BrandID });
        }

        public async Task<BeerDto> GetById(int id)
        {
            //  var beer = await _context.Beers.FindAsync(id);

            var beer = await _beerRepository.GetById(id);

            if (beer != null)
            {
                var beerDto = new BeerDto
                {
                    Id = beer.BeerID,
                    Name = beer.Name,
                    Alcohol = beer.Alcohol,
                    BrandID = beer.BrandID
                };

                return beerDto;
            }

            return null;
        }

        public async Task<BeerDto> Add(BeerInsertDto beerInsertDto)
        {
            var beer = new Beer()
            {
                Name = beerInsertDto.Name,
                BrandID = beerInsertDto.BrandID,
                Alcohol = beerInsertDto.Alcohol
            };

            //  await _context.Beers.AddAsync(beer);
            await _beerRepository.Add(beer);
            //  await _context.SaveChangesAsync();
            await _beerRepository.Save();

            var beerDto = new BeerDto
            {
                Id = beer.BeerID,
                Name = beer.Name,
                Alcohol = beer.Alcohol,
                BrandID = beer.BrandID
            };

            return beerDto;
        }

        public async Task<BeerDto> Update(int id, BeerUpdateDto beerUpdateDto)
        {
            //  var beer = await _context.Beers.FindAsync(id);

            var beer = await _beerRepository.GetById(id);

            if (beer != null)
            {
                beer.Name = beerUpdateDto.Name;
                beer.Alcohol = beerUpdateDto.Alcohol;
                beer.BrandID = beerUpdateDto.BrandID;

                //  await _context.SaveChangesAsync();
                _beerRepository.Update(beer);
                await _beerRepository.Save();

                var beerDto = new BeerDto
                {
                    Id = beer.BeerID,
                    Name = beer.Name,
                    Alcohol = beer.Alcohol,
                    BrandID = beer.BrandID
                };

                return beerDto;
            }

            return null;
        }

        public async Task<BeerDto> Delete(int id)
        {
            var beer = await _context.Beers.FindAsync(id);

            if (beer != null)
            {
                var beerDto = new BeerDto
                {
                    Id = beer.BeerID,
                    Name = beer.Name,
                    Alcohol = beer.Alcohol,
                    BrandID = beer.BrandID
                };

                _context.Remove(beer);
                await _context.SaveChangesAsync();

                return beerDto;
            }

            return null;
        }
    }
}
