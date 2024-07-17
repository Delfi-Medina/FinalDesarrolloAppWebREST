using Microsoft.EntityFrameworkCore;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class GastoRepositorio
    {
        private readonly AppDbContext _context;

        public GastoRepositorio(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<GastoDTO>> ObtenerTodosLosGastos()
        {
            return await _context.Gastos.ToListAsync();
        }

        public async Task<GastoDTO> ObtenerGastoPorId(int id)
        {
            return await _context.Gastos.FindAsync(id);
        }

        public async Task AgregarGasto(GastoDTO gasto)
        {
            await _context.Gastos.AddAsync(gasto);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarGasto(GastoDTO gasto)
        {
            _context.Gastos.Update(gasto);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarGasto(int id)
        {
            var gasto = await _context.Gastos.FindAsync(id);
            if (gasto != null)
            {
                _context.Gastos.Remove(gasto);
                await _context.SaveChangesAsync();
            }
        }
    }
}
