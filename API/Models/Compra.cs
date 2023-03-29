using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Compra
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }

    public int VehiculoId { get; set; }

    public DateTime FechaCompra { get; set; }

    public int? Cantidad { get; set; }

    public decimal PrecioTotal { get; set; }

    public virtual Usuario Usuario { get; set; } = null!;

    public virtual Vehiculo Vehiculo { get; set; } = null!;
}
