using Microsoft.EntityFrameworkCore;
using WebApiLoteria.Entidades;

namespace WebApiLoteria
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Rifa> Rifas { get; set; }
        public DbSet<Participante> Participantes { get; set;}
    }
}
