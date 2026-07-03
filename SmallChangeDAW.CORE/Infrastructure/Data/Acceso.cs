using System;
using System.Collections.Generic;

namespace SmallChangeDAW.CORE.Infrastructure.Data;

public partial class Acceso
{
    public string? Id { get; set; }

    public string? Usuario { get; set; }

    public string? Contra { get; set; }

    public virtual Empleado? IdNavigation { get; set; }
}
