using Microsoft.EntityFrameworkCore;

namespace Pinta.DAL.EntityFramework;

public class PintaDbContext(DbContextOptions<PintaDbContext> options)
    : DbContext(options)
{
    // Agregar DbSet<T> aquí a medida que se creen las entidades.
    // Ejemplo:
    // public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuración de entidades aquí.
    }
}
