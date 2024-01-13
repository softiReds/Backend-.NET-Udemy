using AutoMapper;
using Backend.DTOs;
using Backend.Models;
using Backend.Repository;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class BeerService : ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto>
    {
        //  private StoreContext _context;
        private IRepository<Beer> _beerRepository;
        private IMapper _mapper;

        public BeerService(/*StoreContext context, */IRepository<Beer> beerRepository, IMapper mapper) 
        {
            //  _context = context;
            _beerRepository = beerRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BeerDto>> Get()
        {
            //  await _context.Beers.Select(e => new BeerDto { Id = e.BeerID, Name = e.Name, Alcohol = e.Alcohol, BrandID = e.BrandID }).ToListAsync();

            var beers = await _beerRepository.Get();

            //  return beers.Select(e => new BeerDto { Id = e.BeerID, Name = e.Name, Alcohol = e.Alcohol, BrandID = e.BrandID });
            return beers.Select(e => _mapper.Map<BeerDto>(e));
        }

        public async Task<BeerDto> GetById(int id)
        {
            //  var beer = await _context.Beers.FindAsync(id);

            var beer = await _beerRepository.GetById(id);

            if (beer != null)
            {
                /*
                var beerDto = new BeerDto
                {
                    Id = beer.BeerID,
                    Name = beer.Name,
                    Alcohol = beer.Alcohol,
                    BrandID = beer.BrandID
                };
                */

                var beerDto = _mapper.Map<BeerDto>(beer);

                return beerDto;
            }

            return null;
        }

        public async Task<BeerDto> Add(BeerInsertDto beerInsertDto)
        {
            /*
            var beer = new Beer()
            {
                Name = beerInsertDto.Name,
                BrandID = beerInsertDto.BrandID,
                Alcohol = beerInsertDto.Alcohol
            };
            */

            var beer = _mapper.Map<Beer>(beerInsertDto);    //  Map<destiny>(origin) -> Mapea un objeto del tipo origin en un objeto del tipo destiny. Hace lo del bloque comentareade de arriba, pero reduce el codigo

            //  await _context.Beers.AddAsync(beer);
            await _beerRepository.Add(beer);
            //  await _context.SaveChangesAsync();
            await _beerRepository.Save();

            /*
            var beerDto = new BeerDto
            {
                Id = beer.BeerID,
                Name = beer.Name,
                Alcohol = beer.Alcohol,
                BrandID = beer.BrandID
            };
            */

            var beerDto = _mapper.Map<BeerDto>(beer);

            return beerDto;
        }

        public async Task<BeerDto> Update(int id, BeerUpdateDto beerUpdateDto)
        {
            //  var beer = await _context.Beers.FindAsync(id);

            var beer = await _beerRepository.GetById(id);

            if (beer != null)
            {
                /*
                beer.Name = beerUpdateDto.Name;
                beer.Alcohol = beerUpdateDto.Alcohol;
                beer.BrandID = beerUpdateDto.BrandID;
                */

                beer = _mapper.Map<BeerUpdateDto, Beer>(beerUpdateDto, beer);   //  Cuando se especifican los dos parametros en el mapper, se edita el objeto existente. No crea un nuevo objeto, por lo que no modifica los atributos que no estén mapeados.
                                                                                //      En este caso, nuestra beer ya tiene un id. Si no especificamos los dos parametros se va a eliminar ese id ya que se va a crear un nuevo objeto. Lo que requerimos es editar, no crear. Basicamente, ignora los campos que no estan en el mapper y modifica los campos que si están en el mapper

                //  await _context.SaveChangesAsync();
                _beerRepository.Update(beer);
                await _beerRepository.Save();

                /*
                var beerDto = new BeerDto
                {
                    Id = beer.BeerID,
                    Name = beer.Name,
                    Alcohol = beer.Alcohol,
                    BrandID = beer.BrandID
                };
                */

                var beerDto = _mapper.Map<BeerDto>(beer);

                return beerDto;
            }

            return null;
        }

        public async Task<BeerDto> Delete(int id)
        {
            //  var beer = await _context.Beers.FindAsync(id);

            var beer = await _beerRepository.GetById(id);

            if (beer != null)
            {
                /*
                var beerDto = new BeerDto
                {
                    Id = beer.BeerID,
                    Name = beer.Name,
                    Alcohol = beer.Alcohol,
                    BrandID = beer.BrandID
                };
                */

                var beerDto = _mapper.Map<BeerDto>(beer);

                //  _context.Remove(beer);
                _beerRepository.Delete(beer);
                //  await _context.SaveChangesAsync();
                await _beerRepository.Save();

                return beerDto;
            }

            return null;
        }
    }
}
