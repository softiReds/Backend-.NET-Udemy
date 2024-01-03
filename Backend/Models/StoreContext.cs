using Microsoft.EntityFrameworkCore;

namespace Backend.Models
{
    public class StoreContext : DbContext
    {
        //  Una forma de inicializar las migraciones es: Abrir la consola de paquetes NuGet, Add-Migration migrationName
        public StoreContext(DbContextOptions<StoreContext> options) : base(options) { }

        public DbSet<Beer> Beers { get; set; }
        public DbSet <Brand> Brands { get; set; }
    }
}
