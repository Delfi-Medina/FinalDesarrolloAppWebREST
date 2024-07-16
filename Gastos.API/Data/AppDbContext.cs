using Gastos.API.Modelo;
using Microsoft.EntityFrameworkCore;

namespace Gastos.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        { 
        }
        
        public DbSet<Gasto> Gastos { get; set; }
    }
}
