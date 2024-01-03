using Microsoft.EntityFrameworkCore;

namespace Backend.Models
{
    public class StoreContext : DbContext
    {
        //  Una forma de inicializar y agregar migraciones es: Abrir la consola de paquetes NuGet, Add-Migration migrationName
        //  Update-Database -> Actualiza la base de datos a la ultima migracion
        public StoreContext(DbContextOptions<StoreContext> options) : base(options) { }

        public DbSet<Beer> Beers { get; set; }
        public DbSet <Brand> Brands { get; set; }
    }
}
