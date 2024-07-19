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
            return await _context.Gastos.Select(g => new GastoDTO
            {
                Id = g.Id,
                Descripcion = g.Descripcion,
                Monto = g.Monto,
                Fecha = g.Fecha
            }).ToListAsync();
        }

        public async Task<GastoDTO> ObtenerGastoPorId(int id)
        {
            var gasto = await _context.Gastos.FindAsync(id);
            if (gasto == null) return null;

            return new GastoDTO
            {
                Id = gasto.Id,
                Descripcion = gasto.Descripcion,
                Monto = gasto.Monto,
                Fecha = gasto.Fecha
            };
        }

        public async Task AgregarGasto(GastoDTO gasto)
        {
            var nuevoGasto = new GastoDTO
            {
                Descripcion = gasto.Descripcion,
                Monto = gasto.Monto,
                Fecha = gasto.Fecha
            };

            await _context.Gastos.AddAsync(nuevoGasto);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarGasto(GastoDTO gasto)
        {
            var existingGasto = await _context.Gastos.FindAsync(gasto.Id);
            if (existingGasto == null) return;

            existingGasto.Descripcion = gasto.Descripcion;
            existingGasto.Monto = gasto.Monto;
            existingGasto.Fecha = gasto.Fecha;

            _context.Gastos.Update(existingGasto);
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
