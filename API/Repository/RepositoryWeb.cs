using Microsoft.EntityFrameworkCore;
using API.Models;
using API.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class RepositoryWeb
    {
        private ImportadoraContext context;

        public RepositoryWeb(ImportadoraContext context)
        {
            this.context = context;
        }

        private int GetMaxIdUsuario()
        {
            if (context.Usuarios.Count() == 0)
            {
                return 1;
            }
            else
            {
                return context.Usuarios.Max(z => z.Id) + 1;
            }
        }

        private bool ExisteCorreo(string correo)
        {
            var consulta = from datos in context.Usuarios
                           where datos.Correo == correo
                           select datos;
            if (consulta.Count() > 0)
            {
                //El email existe en la base de datos
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool RegistrarUsuario(string correo, string password, string nombre, string apellidos, int idRol)
        {
            bool ExisteCorreo = this.ExisteCorreo(correo);
            if (ExisteCorreo)
            {
                return false;
            }
            else
            {
                int idusuario = GetMaxIdUsuario();
                Usuario usuario = new Usuario();
                usuario.Id = idusuario;
                usuario.Correo = correo;
                usuario.Nombre = nombre;
                usuario.Apellido = apellidos;
                usuario.RolId = idRol;
                //GENERAMOS UN SALT ALEATORIO PARA CADA USUARIO
                usuario.Salt = HelperCryptography.GenerateSalt();
                //GENERAMOS SU PASSWORD CON EL SALT
                usuario.Password = HelperCryptography.EncriptarPassword(password, usuario.Salt);
                context.Usuarios.Add(usuario);
                context.SaveChanges();

                return true;
            }

        }

        public Usuario LogInUsuario(string correo, string password)
        {
            Usuario usuario = context.Usuarios.SingleOrDefault(x => x.Correo == correo);
            if (usuario == null)
            {
                return null;
            }
            else
            {
                //Debemos comparar con la base de datos el password haciendo de nuevo el cifrado con cada salt de usuario
                string passUsuario = usuario.Password;
                string salt = usuario.Salt;
                //Ciframos de nuevo para comparar
                string temporal = HelperCryptography.EncriptarPassword(password, salt);

                //Comparamos los arrays para comprobar si el cifrado es el mismo
                bool respuesta = HelperCryptography.compareArrays(passUsuario, temporal);
                if (respuesta == true)
                {
                    return usuario;
                }
                else
                {
                    //Contraseña incorrecta
                    return null;
                }
            }
        }

        public List<Usuario> GetUsuarios()
        {
            var consulta = from datos in context.Usuarios
                           select datos;
            return consulta.ToList();
        }
    }
}