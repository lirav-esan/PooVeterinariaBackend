using System;
using System.Collections.Generic;

namespace SmallChangeDAW.CORE.Infrastructure.Data;

public partial class Registro
{
    public string Id { get; set; } = null!;

    public DateOnly? Fecha { get; set; }

    public TimeOnly? Hora { get; set; }

    public string? IdMascota { get; set; }

    public string? IdEmpleado { get; set; }

    public virtual Empleado? IdEmpleadoNavigation { get; set; }

    public virtual Paciente? IdMascotaNavigation { get; set; }
}
