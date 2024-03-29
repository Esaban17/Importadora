﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportadoraModels
{
    public class Compra
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }

        public int VehiculoId { get; set; }

        public DateTime FechaCompra { get; set; }

        public int? Cantidad { get; set; }

        public decimal PrecioTotal { get; set; }
    }
}
