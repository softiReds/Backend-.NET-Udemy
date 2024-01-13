using AutoMapper;
using Backend.DTOs;
using Backend.Models;

namespace Backend.Automappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<BeerInsertDto, Beer>();   //  CreateMap<origin, destiny>() -> Crea un mapping para mapear objetos del origin en objetos del destiny
                                                //      Cuando el origin y el destiny tienen los mismo nombres en los atributos a mapear, es sificiente con la definición del mapping
            CreateMap<Beer, BeerDto>()
                .ForMember(dto => dto.Id,
                            map => map.MapFrom(b => b.BeerID));
            //  ForMember(destiny => destiny.PropertyName, origin => origin.MapFrom(e => e.PropertyNameOrigin)) -> Configura un mapper para propiedades con diferentes nombres, pero continua mapenado automaticamente las propiedades con nombres iguales
        }
    }
}
