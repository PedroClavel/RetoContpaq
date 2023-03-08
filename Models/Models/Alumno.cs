using System;
using System.Collections.Generic;

namespace Models.Models;

public partial class Alumno
{
    public int IdAlumno { get; set; }

    public string Nombres { get; set; } = null!;

    public string ApellidoPaterno { get; set; } = null!;

    public string ApellidoMaterno { get; set; } = null!;

    public int Edad { get; set; }

    public int Grado { get; set; }

    public string Grupo { get; set; } = null!;

    public int Telefono { get; set; }

    public DateTime FechaAlta { get; set; }

    public DateTime? FechaModificacion { get; set; }
}
