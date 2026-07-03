using System;
using System.Collections.Generic;

namespace SmallChangeDAW.CORE.Infrastructure.Data;

public partial class TiposMascotum
{
    public string Id { get; set; } = null!;

    public string? Nombre { get; set; }

    public virtual ICollection<Paciente> Pacientes { get; set; } = new List<Paciente>();
}
