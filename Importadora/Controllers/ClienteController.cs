using Importadora.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Importadora.Controllers
{
    public class ClienteController : Controller
    {
        // GET: ClienteController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ClienteController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // GET: ClienteController/Edit
        public ActionResult Edit()
        {
            return View();
        }

        // GET: ClienteController/List
        public ActionResult List()
        {
            return View();
        }

        // POST: ClienteController/Create
        [HttpPost]
        public IActionResult Create(IFormCollection formCollection)
        {
            var db = new ImportadoraContext();

            var Cliente = new Cliente { 
                Nombre = formCollection["Nombre"],
                Apellido = formCollection["Apellido"],
                Correo = formCollection["Correo"],
                Password = formCollection["Password"],
                Direccion = formCollection["Direccion"],
                Ciudad = formCollection["Ciudad"],
                Estado = formCollection["Estado"],
                CodigoPostal = formCollection["CodigoPostal"],
                Telefono = formCollection["Telefono"]
            };

            db.Clientes.Add(Cliente);
            db.SaveChanges();
       
            return View();
        }

    }
}
