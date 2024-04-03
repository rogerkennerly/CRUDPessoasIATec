using Microsoft.EntityFrameworkCore;
using TesteIATec.Models;

namespace TesteIATec.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { 
        
        }

        public DbSet<Pessoa> Pessoas { get; set; }
    }
}
