using Importadora.Models;
using Microsoft.AspNetCore.Mvc;

namespace Importadora.Controllers
{
    public class VehiculoController : Controller
    {
        // GET: VehiculoController
        public ActionResult Index()
        {
            return View();
        }

        // GET: VehiculoController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // GET: VehiculoController/Edit
        public ActionResult Edit()
        {
            return View();
        }

        // GET: VehiculoController/List
        public async Task<IActionResult> List()
        {
            IEnumerable<ImportadoraModels.Vehiculo> vehiculos = await Services.VehiculoService.GetVehiculos();

            return View(vehiculos);
        }

        // POST: VehiculoController/Create
        [HttpPost]
        public async Task<IActionResult> Create(IFormCollection formCollection)
        {
            var db = new ImportadoraContext();

            var newVehiculo = new ImportadoraModels.Vehiculo
            {
                Marca = formCollection["Marca"],
                Modelo = formCollection["Modelo"],
                Anio = Convert.ToInt32(formCollection["Anio"]),
                Precio = Convert.ToDecimal(formCollection["Precio"]),
                Cantidad = Convert.ToInt32(formCollection["Cantidad"])
            };

            await Services.VehiculoService.Create(newVehiculo);

            return View();
        }
    }
}
