using Microsoft.EntityFrameworkCore;
using Pinta.Domain.Auth;
using Pinta.Domain.Security;
using Pinta.Domain.Profile;

namespace Pinta.DAL.EntityFramework;

public class PintaDbContext(DbContextOptions<PintaDbContext> options)
    : DbContext(options)
{
    // Agregar DbSet<T> aqu� a medida que se creen las entidades.
    // Ejemplo:
    // public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuraci�n de entidades aqu�.
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Person> Persons => Set<Person>();
    public DbSet<Ban> Bans => Set<Ban>();

    public DbSet<Avatar> Avatars => Set<Avatar>();
    public DbSet<Portadaperfil> Portadaperfiles => Set<Portadaperfil>();
    public DbSet<Follow> Follows => Set<Follow>();
}
