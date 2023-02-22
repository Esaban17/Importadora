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
        public ActionResult List()
        {
            return View();
        }

        // POST: CompraController/Create
        [HttpPost]
        public IActionResult Create(IFormCollection formCollection)
        {
            var db = new ImportadoraContext();

            var Compra = new Compra
            {
                ClienteId = Convert.ToInt32(formCollection["ClienteId"]),
                VehiculoId = Convert.ToInt32(formCollection["VehiculoId"]),
                FechaCompra = Convert.ToDateTime(formCollection["FechaCompra"]),
                Cantidad = Convert.ToInt32(formCollection["Cantidad"]),
                PrecioTotal = Convert.ToDecimal(formCollection["PrecioTotal"])
            };

            db.Compras.Add(Compra);
            db.SaveChanges();

            return View();
        }
    }
}
