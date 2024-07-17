using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Modelo;



namespace Gastos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GastosController : ControllerBase
    {
        private readonly GastoRepositorio _gastoRepositorio;

        public GastosController(GastoRepositorio gastoRepositorio)
        {
            _gastoRepositorio = gastoRepositorio;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GastoDTO>>> GetGastos()
        {
            var gastos = await _gastoRepositorio.ObtenerTodosLosGastos();
            return Ok(gastos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GastoDTO>> GetGasto(int id)
        {
            var gasto = await _gastoRepositorio.ObtenerGastoPorId(id);
            if (gasto == null) return NotFound();
            return Ok(gasto);
        }

        [HttpPost]
        public async Task<ActionResult> PostGasto(GastoDTO gasto)
        {
            await _gastoRepositorio.AgregarGasto(gasto);
            return CreatedAtAction(nameof(GetGasto), new { id = gasto.Id }, gasto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutGasto(int id, GastoDTO gasto)
        {
            if (id != gasto.Id) return BadRequest();
            await _gastoRepositorio.ActualizarGasto(gasto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGasto(int id)
        {
            var gasto = await _gastoRepositorio.ObtenerGastoPorId(id);
            if (gasto == null) return NotFound();
            await _gastoRepositorio.EliminarGasto(id);
            return NoContent();
        }
    }
}

