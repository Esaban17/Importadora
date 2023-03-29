using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Mysqlx.Crud.Order.Types;
using static System.Runtime.InteropServices.JavaScript.JSType;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        // GET: api/<UsuarioController>
        [HttpGet]
        [Route("GetUsuarios")]
        public async Task<IEnumerable<ImportadoraModels.Usuario>> GetUsuarios()
        {
            ImportadoraContext _importadoraContext = new ImportadoraContext();
            IEnumerable<ImportadoraModels.Usuario> Usuarios = _importadoraContext.Usuarios.Select(s =>
            new ImportadoraModels.Usuario
            {
                Id = s.Id,
                Nombre = s.Nombre,
                Apellido = s.Apellido,
                Correo = s.Correo,
                Password = s.Password,
                Direccion = s.Direccion,
                Ciudad = s.Ciudad,
                Estado = s.Estado,
                CodigoPostal = s.CodigoPostal,
                Telefono = s.Telefono,
                RolId = s.RolId
            }
            ).ToList();
            return Usuarios;
        }

        // GET api/<UsuarioController>/5
        [Route("GetUsuario")]
        [HttpGet]
        public async Task<ImportadoraModels.Usuario> GetUsuario(int id)
        {
            ImportadoraContext _importadoraContext = new ImportadoraContext();
            ImportadoraModels.Usuario Usuario = await _importadoraContext.Usuarios.Select(s =>
            new ImportadoraModels.Usuario
            {
                Id = s.Id,
                Nombre = s.Nombre,
                Apellido = s.Apellido,
                Correo = s.Correo,
                Password = s.Password,
                Direccion = s.Direccion,
                Ciudad = s.Ciudad,
                Estado = s.Estado,
                CodigoPostal = s.CodigoPostal,
                Telefono = s.Telefono,
                RolId = s.RolId
            }
            ).FirstAsync(s => s.Id == id);
            return Usuario;
        }

        // POST api/<UsuarioController>
        [Route("Create")]
        [HttpPost]
        public async Task<ImportadoraModels.GeneralResult> Create(ImportadoraModels.Usuario Usuario)
        {
            ImportadoraModels.GeneralResult generalResult = new ImportadoraModels.GeneralResult()
            {
                Result = false
            };

            try
            {
                ImportadoraContext _importadoraContext = new ImportadoraContext();
                Models.Usuario newClient = new Models.Usuario
                {
                    Nombre = Usuario.Nombre,
                    Apellido = Usuario.Apellido,
                    Correo = Usuario.Correo,
                    Password = Usuario.Password,
                    Direccion = Usuario.Direccion,
                    Ciudad = Usuario.Ciudad,
                    Estado = Usuario.Estado,
                    CodigoPostal = Usuario.CodigoPostal,
                    Telefono = Usuario.Telefono,
                    RolId = Usuario.RolId,
                };
                _importadoraContext.Usuarios.Add(newClient);
                await _importadoraContext.SaveChangesAsync();
                generalResult.Result = true;
            }
            catch (Exception ex)
            {
                generalResult.Result = false;
                generalResult.ErrorMessage = ex.Message;
            }
            return generalResult;
        }

        // PUT api/<UsuarioController>/5
        [Route("Update")]
        [HttpPut]
        public async Task<ImportadoraModels.GeneralResult> Update(ImportadoraModels.Usuario Usuario)
        {
            ImportadoraModels.GeneralResult generalResult = new ImportadoraModels.GeneralResult()
            {
                Result = false
            };

            try
            {
                ImportadoraContext _importadoraContext = new ImportadoraContext();
                var UsuarioToUpdate = await _importadoraContext.Usuarios.FindAsync(Usuario.Id);
                if (UsuarioToUpdate != null)
                {
                    UsuarioToUpdate.Nombre = Usuario.Nombre;
                    UsuarioToUpdate.Apellido = Usuario.Apellido;
                    UsuarioToUpdate.Correo = Usuario.Correo;
                    UsuarioToUpdate.Password = Usuario.Password;
                    UsuarioToUpdate.Direccion = Usuario.Direccion;
                    UsuarioToUpdate.Ciudad = Usuario.Ciudad;
                    UsuarioToUpdate.Estado = Usuario.Estado;
                    UsuarioToUpdate.CodigoPostal = Usuario.CodigoPostal;
                    UsuarioToUpdate.Telefono = Usuario.Telefono;

                    await _importadoraContext.SaveChangesAsync();
                    generalResult.Result = true;
                }
                else
                {
                    generalResult.ErrorMessage = $"Usuario con id={Usuario.Id} no encontrado.";
                }
            }
            catch (Exception ex)
            {
                generalResult.ErrorMessage = ex.Message;
            }
            return generalResult;
        }

        // DELETE api/<UsuarioController>/5
        [Route("Delete")]
        [HttpDelete]
        public async Task<ImportadoraModels.GeneralResult> Delete(int id)
        {
            ImportadoraModels.GeneralResult generalResult = new ImportadoraModels.GeneralResult()
            {
                Result = false
            };

            using (var dbContext = new ImportadoraContext())
            {
                using (var transaction = dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        var Usuario = await dbContext.Usuarios.FindAsync(id);

                        if (Usuario != null)
                        {

                            dbContext.Usuarios.Remove(Usuario);

                            dbContext.SaveChanges();

                            transaction.Commit();
                        }
                        else
                        {
                            generalResult.ErrorMessage = $"Usuario con id={id} no encontrado.";
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            return generalResult;
        }
    }
}
