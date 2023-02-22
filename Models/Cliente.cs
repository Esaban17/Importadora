using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Cliente
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string Apellido { get; set; } = null!;

        public string Correo { get; set; } = null!;

        public string Contraseña { get; set; } = null!;

        public string Dirección { get; set; } = null!;

        public string? Ciudad { get; set; }

        public string? Estado { get; set; }

        public string? CódigoPostal { get; set; }

        public string? Teléfono { get; set; }
    }
}
