using Importadora.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Importadora.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: UsuarioController
        public ActionResult Index()
        {
            return View();
        }

        // GET: UsuarioController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // GET: UsuarioController/Edit
        public ActionResult Edit()
        {
            return View();
        }

        // GET: UsuarioController/List
        public async Task<IActionResult> List()
        {
            IEnumerable<ImportadoraModels.Usuario> usuarios = await Services.UsuarioService.GetUsuarios();

            return View(usuarios);
        }

        // POST: UsuarioController/Create
        [HttpPost]
        public IActionResult Create(IFormCollection formCollection)
        {
            var db = new ImportadoraContext();

            var Usuario = new Usuario { 
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

            db.Usuarios.Add(Usuario);
            db.SaveChanges();
       
            return View();
        }

    }
}
