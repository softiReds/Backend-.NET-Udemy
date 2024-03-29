﻿using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository
{
    public class BeerRepository : IRepository<Beer>
    {
        private StoreContext _context;

        public BeerRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Beer>> Get() => await _context.Beers.ToListAsync();

        public async Task<Beer> GetById(int id) => await _context.Beers.FindAsync(id);

        public async Task Add(Beer entity) => await _context.Beers.AddAsync(entity);

        public void Update(Beer entity)
        {
            _context.Beers.Attach(entity);  //  Attach(entity) -> Adjunta la entidad al contexto cuando esta ya existe dentro del contexto (util para UPDATE)
            _context.Beers.Entry(entity).State = EntityState.Modified;  //  Entry(entity).State = EntityState.State -> Especifica un estado para que el contexto realice los cambios en la entidad
                                                                        //                                  ...Modified -> Especifica que se hizo una modificación
        }

        public void Delete(Beer entity) => _context.Remove(entity);

        public async Task Save() => await _context.SaveChangesAsync();
    }
}
