using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using NuGet.Common;
using Microsoft.AspNetCore.Authorization;
using API.Services;
using API.Helpers;
using System.Security.Cryptography;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly string secretKey;

        public UsuarioController(IConfiguration config)
        {
            secretKey = config.GetSection("settings").GetSection("secretKey").ToString();
        }


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
        public async Task<ImportadoraModels.GeneralResult> Create(ImportadoraModels.Usuario usuario)
        {
            ImportadoraModels.GeneralResult generalResult = new ImportadoraModels.GeneralResult()
            {
                Result = false
            };

            try
            {
                ImportadoraContext _importadoraContext = new ImportadoraContext();

                var response = await ExisteCorreo(usuario.Correo);

                if (response)
                {
                    generalResult.Result = false;
                    generalResult.ErrorMessage = "ERROR ya existe un usuario con ese correo";
                }
                else
                {
                    //GENERAMOS UN SALT ALEATORIO PARA CADA USUARIO
                    var Salt = HelperCryptography.GenerateSalt();
                    //GENERAMOS SU PASSWORD CON EL SALT
                    string encryptPassword = Encrypt(usuario.Password, Salt);

                    Models.Usuario newUsuario = new Models.Usuario
                    {
                        Nombre = usuario.Nombre,
                        Apellido = usuario.Apellido,
                        Correo = usuario.Correo,
                        Password = encryptPassword,
                        Salt = Salt,
                        Direccion = usuario.Direccion,
                        Ciudad = usuario.Ciudad,
                        Estado = usuario.Estado,
                        CodigoPostal = usuario.CodigoPostal,
                        Telefono = usuario.Telefono,
                        RolId = usuario.RolId
                    };
                    _importadoraContext.Usuarios.Add(newUsuario);
                    await _importadoraContext.SaveChangesAsync();
                    generalResult.Result = true;
                }
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

        // LOGIN USUARIOS api/<UsuarioController>/5
        [Route("Authenticate")]
        [HttpPost]
        public async Task<IActionResult> Validate(ImportadoraModels.Usuario usuario)
        {
            ImportadoraModels.GeneralResult generalResult = new ImportadoraModels.GeneralResult()
            {
                Result = false
            };

            try
            {

                ImportadoraContext _importadoraContext = new ImportadoraContext();

                Usuario userDB = await _importadoraContext.Usuarios.SingleOrDefaultAsync(x => x.Correo.ToLower() == usuario.Correo.ToLower());

                //Debemos comparar con la base de datos el password haciendo de nuevo el cifrado con cada salt de usuario
                string passUsuario = userDB.Password;
                string salt = userDB.Salt;
                //Ciframos de nuevo para comparar
                string passTemporal = Encrypt(usuario.Password, salt);

                //Comparamos los arrays para comprobar si el cifrado es el mismo
                bool isEquals = passTemporal.Equals(passUsuario);
                if (isEquals)
                {
                    var keyBytes = Encoding.ASCII.GetBytes(secretKey);
                    var claims = new ClaimsIdentity();

                    claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario.Correo));

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = claims,
                        Expires = DateTime.UtcNow.AddMinutes(10),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature),
                    };

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

                    string token = tokenHandler.WriteToken(tokenConfig);

                    return StatusCode(StatusCodes.Status200OK, new { token });
                }
                else
                {
                    //Contraseña incorrecta
                    return StatusCode(StatusCodes.Status401Unauthorized, new { token = "" });
                }
            }
            catch (Exception ex)
            {
                generalResult.Result = false;
                generalResult.ErrorMessage = ex.Message;
            }
            return StatusCode(StatusCodes.Status401Unauthorized, new { token = "" });
        }

        private async Task<bool> ExisteCorreo(string correo)
        {
            ImportadoraContext _importadoraContext = new ImportadoraContext();
            ImportadoraModels.Usuario foundUser = await _importadoraContext.Usuarios.Select(s =>
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
            ).FirstOrDefaultAsync(s => s.Correo == correo);

            if (foundUser != null)
            {
                //El email existe en la base de datos
                return true;
            }
            else
            {
                return false;
            }
        }

        private string Encrypt(string password, string randomSalt)
        {
            string hash = randomSalt;
            byte[] data = UTF8Encoding.UTF8.GetBytes(password);

            MD5 md5 = MD5.Create();
            TripleDES tripDES = TripleDES.Create();

            tripDES.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            tripDES.Mode = CipherMode.ECB;

            ICryptoTransform cryptoTransform = tripDES.CreateEncryptor();
            byte[] result =  cryptoTransform.TransformFinalBlock(data, 0, data.Length);

            return Convert.ToBase64String(result);
        }

        private string Decrypt(string encryptPass, string randomSalt)
        {
            string hash = randomSalt;
            byte[] data = Convert.FromBase64String(encryptPass);

            MD5 md5 = MD5.Create();
            TripleDES tripDES = TripleDES.Create();

            tripDES.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            tripDES.Mode = CipherMode.ECB; 

            ICryptoTransform cryptoTransform = tripDES.CreateDecryptor();
            byte[] result = cryptoTransform.TransformFinalBlock(data, 0, data.Length);


            return UTF8Encoding.UTF8.GetString(result);
        }

    }
}
