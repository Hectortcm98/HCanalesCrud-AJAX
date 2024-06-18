using System;
using System.Collections.Generic;

namespace DL;

public partial class EntidadFederativa
{
    public int IdEstado { get; set; }

    public string Estado { get; set; } = null!;

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}
