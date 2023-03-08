using System;
using System.Collections.Generic;

namespace Models.Models;

public partial class Sesion
{
    public int IdLogin { get; set; }

    public string Usuario { get; set; } = null!;

    public string Pass { get; set; } = null!;

    public DateTime FechaAlta { get; set; }

    public DateTime? FechaModificacion { get; set; }
}
