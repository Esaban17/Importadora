using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportadoraModels
{
    public class Usuario
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string Apellido { get; set; } = null!;

        public string Correo { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Direccion { get; set; } = null!;

        public string? Ciudad { get; set; }

        public string? Estado { get; set; }

        public string? CodigoPostal { get; set; }

        public string? Telefono { get; set; }

        public int RolId { get; set; }
    }
}
