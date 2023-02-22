using System;
using System.Collections.Generic;

namespace Importadora.Models;

public partial class Compra
{
    public int Id { get; set; }

    public int ClienteId { get; set; }

    public int VehiculoId { get; set; }

    public DateTime FechaCompra { get; set; }

    public int? Cantidad { get; set; }

    public decimal PrecioTotal { get; set; }

    public virtual Cliente Cliente { get; set; } = null!;

    public virtual Vehiculo Vehiculo { get; set; } = null!;
}
