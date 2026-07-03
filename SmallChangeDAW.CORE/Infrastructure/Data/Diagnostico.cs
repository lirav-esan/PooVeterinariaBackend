using System;
using System.Collections.Generic;

namespace SmallChangeDAW.CORE.Infrastructure.Data;

public partial class Diagnostico
{
    public string? Id { get; set; }

    public string? IdMedico { get; set; }

    public string? Diagnostico1 { get; set; }

    public virtual TiposDiagnostico? Diagnostico1Navigation { get; set; }

    public virtual Empleado? IdMedicoNavigation { get; set; }

    public virtual Registro? IdNavigation { get; set; }
}
