using System;
using System.Collections.Generic;

namespace SmallChangeDAW.CORE.Infrastructure.Data;

public partial class Vacuna
{
    public string? IdPaciente { get; set; }

    public string? IdVacuna { get; set; }

    public virtual Paciente? IdPacienteNavigation { get; set; }

    public virtual TiposVacuna? IdVacunaNavigation { get; set; }
}
