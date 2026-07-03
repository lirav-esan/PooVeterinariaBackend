using System;
using System.Collections.Generic;

namespace SmallChangeDAW.CORE.Infrastructure.Data;

public partial class Estetica
{
    public string? Id { get; set; }

    public string? IdEmpleado { get; set; }

    public string? Concepto { get; set; }

    public virtual Concepto? ConceptoNavigation { get; set; }

    public virtual Empleado? IdEmpleadoNavigation { get; set; }

    public virtual Registro? IdNavigation { get; set; }
}
