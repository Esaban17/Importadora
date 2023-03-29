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

        // GET: CompraController/List
        public async Task<IActionResult> List()
        {

            ViewBag.Vehiculos = await Services.VehiculoService.GetVehiculos(); ;

            IEnumerable<ImportadoraModels.Compra> compras = await Services.CompraService.GetCompras();

            return View(compras);
        }

        // POST: CompraController/Create
        [HttpPost]
        public async Task<IActionResult> Create(IFormCollection formCollection)
        {
            var db = new ImportadoraContext();

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
    }
}
