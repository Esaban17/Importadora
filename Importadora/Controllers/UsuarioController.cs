using Importadora.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Utilities;

namespace Importadora.Controllers
{
    public class UsuarioController : Controller
    {

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

        // GET: UsuarioController/Details
        public ActionResult Details()
        {
            return View();
        }

        // GET: UsuarioController/Delete
        public ActionResult Delete()
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
        public async Task<IActionResult> Create(IFormCollection formCollection)
        {

            var rolId = 3;
            if (Convert.ToInt32(formCollection["RolId"]) < 3)
            {
                rolId = Convert.ToInt32(formCollection["RolId"]);
            }

            var newUsuario = new ImportadoraModels.Usuario { 
                Nombre = formCollection["Nombre"],
                Apellido = formCollection["Apellido"],
                Correo = formCollection["Correo"],
                Password = formCollection["Password"],
                Direccion = formCollection["Direccion"],
                Ciudad = formCollection["Ciudad"],
                Estado = formCollection["Estado"],
                CodigoPostal = formCollection["CodigoPostal"],
                Telefono = formCollection["Telefono"],
                RolId = rolId
            };

            var response = await Services.UsuarioService.Create(newUsuario, "");

            if (response.Result)
            {
                return View("Views/Home/Login.cshtml", response.Result);
            }
            else
            {
                return View("Views/Home/Register.cshtml", response.Result);
            }
      
        }

        // POST: UsuarioController/Login
        [HttpPost]
        public async Task<IActionResult> Login(IFormCollection formCollection)
        {

            var loginUser = new ImportadoraModels.Usuario
            {
                Correo = formCollection["Correo"],
                Password = formCollection["Password"]
            };

            var response = await Services.UsuarioService.Login(loginUser);

            if (response.Token != "")
            {
                return View("Views/Home/Index.cshtml");
            }
            else
            {
                return View("Views/Home/Login.cshtml");
            }

        }


        // PUT: UsuarioController/Update
        [HttpPut]
        public async Task<IActionResult> Edit(IFormCollection formCollection)
        {

            var updateUsuario = await Services.UsuarioService.GetUsuario(Convert.ToInt32(formCollection["Id"]));

            if (updateUsuario != null)
            {

                updateUsuario.Nombre = formCollection["Nombre"];
                updateUsuario.Apellido = formCollection["Apellido"];
                updateUsuario.Correo = formCollection["Correo"];
                updateUsuario.Password = formCollection["Password"];
                updateUsuario.Direccion = formCollection["Direccion"];
                updateUsuario.Ciudad = formCollection["Ciudad"];
                updateUsuario.Estado  = formCollection["Estado"];
                updateUsuario.CodigoPostal  = formCollection["CodigoPostal"];
                updateUsuario.Telefono = formCollection["Telefono"];
                updateUsuario.RolId = Convert.ToInt32(formCollection["RolId"]);

                await Services.UsuarioService.Update(updateUsuario);

            }

            return View();
        }

    }
}
