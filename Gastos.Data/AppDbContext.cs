using Microsoft.EntityFrameworkCore;
using Modelo;
using System.Collections.Generic;

namespace Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<GastoDTO> Gastos { get; set; }
    }
}
