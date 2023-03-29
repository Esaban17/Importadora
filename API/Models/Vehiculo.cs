using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Vehiculo
{
    public int Id { get; set; }

    public string Marca { get; set; } = null!;

    public string Modelo { get; set; } = null!;

    public int Anio { get; set; }

    public decimal Precio { get; set; }

    public int? Cantidad { get; set; }

    public virtual ICollection<Compra> Compras { get; } = new List<Compra>();
}
