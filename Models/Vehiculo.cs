using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportadoraModels
{
    public class Vehiculo
    {
        public int Id { get; set; }

        public string Marca { get; set; } = null!;

        public string Modelo { get; set; } = null!;

        public int Anio { get; set; }

        public decimal Precio { get; set; }

        public int? Cantidad { get; set; }
    }
}
