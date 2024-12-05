using Microsoft.EntityFrameworkCore;
namespace zadanie_rekrutacyjne
{
    public class ToDoBaza : DbContext
    {
        public ToDoBaza(DbContextOptions<ToDoBaza> options):base(options) {

        }
        public DbSet<Todo> Todos => Set<Todo>();
    }
}
