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

        // GET: VehiculoController/Details
        public ActionResult Details()
        {
            return View();
        }

        // GET: VehiculoController/Delete
        public ActionResult Delete()
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

        // PUT: VehiculoController/Update
        [HttpPut]
        public async Task<IActionResult> Edit(IFormCollection formCollection)
        {

            var updateVehiculo = await Services.VehiculoService.GetVehiculo(Convert.ToInt32(formCollection["Id"]));

            if (updateVehiculo != null)
            {

                updateVehiculo.Marca = formCollection["Marca"];
                updateVehiculo.Modelo = formCollection["Modelo"];
                updateVehiculo.Anio = Convert.ToInt32(formCollection["Anio"]);
                updateVehiculo.Precio = Convert.ToDecimal(formCollection["Precio"]);
                updateVehiculo.Cantidad = Convert.ToInt32(formCollection["Cantidad"]);

                await Services.VehiculoService.Update(updateVehiculo);

            }

            return View();
        }
    }
}
