using System;
using System.Collections.Generic;

namespace SmallChangeDAW.CORE.Infrastructure.Data;

public partial class Paciente
{
    public string Id { get; set; } = null!;

    public string? IdTitular { get; set; }

    public string? Nombre { get; set; }

    public string? Apellidos { get; set; }

    public string? Tipo { get; set; }

    public DateOnly? FechaNacimiento { get; set; }

    public DateOnly? FechaFallecimiento { get; set; }

    public string? Sexo { get; set; }

    public string? Color { get; set; }

    public bool? Esterilizado { get; set; }

    public int? Longitud { get; set; }

    public int? Altura { get; set; }

    public int? Peso { get; set; }

    public string? Morfologia { get; set; }

    public string? GrupoSanguineo { get; set; }

    public bool? Microchip { get; set; }

    public bool? Tatuaje { get; set; }

    public string? Observaciones { get; set; }

    public virtual Cliente? IdTitularNavigation { get; set; }

    public virtual ICollection<Registro> Registros { get; set; } = new List<Registro>();

    public virtual TiposMascotum? TipoNavigation { get; set; }
}
