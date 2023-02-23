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
        public ActionResult List()
        {
            var db = new ImportadoraContext();
            var vehiculos = db.Vehiculos.ToList();

           return View(vehiculos);
        }

        // POST: VehiculoController/Create
        [HttpPost]
        public IActionResult Create(IFormCollection formCollection)
        {
            var db = new ImportadoraContext();

            var Vehiculo = new Vehiculo
            {
                Marca = formCollection["Marca"],
                Modelo = formCollection["Modelo"],
                Anio = Convert.ToInt32(formCollection["Anio"]),
                Precio = Convert.ToDecimal(formCollection["Precio"]),
                Cantidad = Convert.ToInt32(formCollection["Cantidad"])
            };

            db.Vehiculos.Add(Vehiculo);
            db.SaveChanges();

            return View();
        }
    }
}
