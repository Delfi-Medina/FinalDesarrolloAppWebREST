using Microsoft.AspNetCore.Mvc;
using Modelo;


namespace Gastos.MVC.Controllers
{
    public class GastosController : Controller
    {
        private readonly HttpClient _httpClient;

        public GastosController(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpClient.BaseAddress = new Uri("https://localhost:7037/api/Gastos/");
        }

        // GET: Gastos
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("");
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, "Error al obtener los gastos.");
            }

            var gastos = await response.Content.ReadFromJsonAsync<List<GastoDTO>>();
            return View(gastos);
        }
        // GET: Gastos/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"{id}");
            if (!response.IsSuccessStatusCode)
            {
                return NotFound("Gasto no encontrada.");
            }
            var gasto = await response.Content.ReadFromJsonAsync<GastoDTO>();
            return View(gasto);
        }

        // GET: Gastos/Create
        public IActionResult Create()
        {
            var gasto = new GastoDTO()
            {
                Fecha = DateTime.Today
            };
            return View(gasto);
        }


        // POST: Gastos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GastoDTO gasto)
        {
            if (ModelState.IsValid)
            {
                if (gasto.Fecha == DateTime.MinValue)
                {
                    gasto.Fecha = DateTime.Today; 
                }
                var response = await _httpClient.PostAsJsonAsync("", gasto);
                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode((int)response.StatusCode, "Error al crear el gasto.");
                }
                return RedirectToAction(nameof(Index));
            }
            
            return View(gasto);
        }


        // GET: Gastos/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"{id}");
            if (!response.IsSuccessStatusCode)
            {
                return NotFound("Gasto no encontrado.");
            }
            var gasto = await response.Content.ReadFromJsonAsync<GastoDTO>();
            return View(gasto);
        }

        // POST: Gastos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GastoDTO gasto)
        {
            if (id != gasto.Id)
            {
                return BadRequest("ID de gasto no coincide.");
            }

            if (ModelState.IsValid)
            {
                if (gasto.Fecha == DateTime.MinValue)
                {
                    gasto.Fecha = DateTime.Today; 
                }
                var response = await _httpClient.PutAsJsonAsync($"{id}", gasto);
                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode((int)response.StatusCode, "Error al actualizar el gasto.");
                }
                return RedirectToAction(nameof(Index));
            }
            return View(gasto);
        }

        // POST: Gastos/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"{id}");
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, "Error al eliminar el gasto.");
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
