using Importadora.Models;
using Microsoft.AspNetCore.Mvc;

namespace Importadora.Controllers
{
    public class CompraController : Controller
    {
        // GET: CompraController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CompraController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // GET: CompraController/Edit
        public ActionResult Edit()
        {
            return View();
        }

        // GET: CompraController/Details
        public ActionResult Details()
        {
            return View();
        }

        // GET: CompraController/Delete
        public ActionResult Delete()
        {
            return View();
        }

        // GET: CompraController/List
        public async Task<IActionResult> List()
        {

            ViewBag.Vehiculos = await Services.VehiculoService.GetVehiculos();

            IEnumerable<ImportadoraModels.Compra> compras = await Services.CompraService.GetCompras();

            return View(compras);
        }

        // POST: CompraController/Create
        [HttpPost]
        public async Task<IActionResult> Create(IFormCollection formCollection)
        {
            var newCompra = new ImportadoraModels.Compra
            {
                UsuarioId = Convert.ToInt32(formCollection["UsuarioId"]),
                VehiculoId = Convert.ToInt32(formCollection["VehiculoId"]),
                FechaCompra = Convert.ToDateTime(formCollection["FechaCompra"]),
                Cantidad = Convert.ToInt32(formCollection["Cantidad"]),
                PrecioTotal = Convert.ToDecimal(formCollection["PrecioTotal"])
            };

            await Services.CompraService.Create(newCompra);

            return View();
        }

        // PUT: CompraController/Update
        [HttpPut]
        public async Task<IActionResult> Edit(IFormCollection formCollection)
        {

            var updateCompra = await Services.CompraService.GetCompra(Convert.ToInt32(formCollection["Id"]));

            if (updateCompra != null)
            {

                updateCompra.UsuarioId = Convert.ToInt32(formCollection["UsuarioId"]);
                updateCompra.VehiculoId = Convert.ToInt32(formCollection["VehiculoId"]);
                updateCompra.FechaCompra = Convert.ToDateTime(formCollection["FechaCompra"]);
                updateCompra.Cantidad = Convert.ToInt32(formCollection["Cantidad"]);
                updateCompra.PrecioTotal = Convert.ToDecimal(formCollection["PrecioTotal"]);

                await Services.CompraService.Update(updateCompra);

            }

            return View();
        }
    }
}
