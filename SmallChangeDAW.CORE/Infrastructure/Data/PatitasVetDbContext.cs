using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SmallChangeDAW.CORE.Infrastructure.Data;

public partial class PatitasVetDbContext : DbContext
{
    public PatitasVetDbContext()
    {
    }

    public PatitasVetDbContext(DbContextOptions<PatitasVetDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Acceso> Accesos { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Concepto> Conceptos { get; set; }

    public virtual DbSet<Diagnostico> Diagnosticos { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Estetica> Esteticas { get; set; }

    public virtual DbSet<Paciente> Pacientes { get; set; }

    public virtual DbSet<Registro> Registros { get; set; }

    public virtual DbSet<TiposDiagnostico> TiposDiagnosticos { get; set; }

    public virtual DbSet<TiposMascotum> TiposMascota { get; set; }

    public virtual DbSet<TiposVacuna> TiposVacunas { get; set; }

    public virtual DbSet<Vacuna> Vacunas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=PatitasVet;Integrated Security=true;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Acceso>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Contra)
                .HasMaxLength(15)
                .HasColumnName("contra");
            entity.Property(e => e.Id)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ID");
            entity.Property(e => e.Usuario)
                .HasMaxLength(10)
                .HasColumnName("usuario");

            entity.HasOne(d => d.IdNavigation).WithMany()
                .HasForeignKey(d => d.Id)
                .HasConstraintName("fk_accesos_empleados");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cliente__3214EC271A3FF251");

            entity.ToTable("Cliente");

            entity.Property(e => e.Id)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ID");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(20)
                .HasColumnName("apellidos");
            entity.Property(e => e.Ciudad)
                .HasMaxLength(15)
                .HasColumnName("ciudad");
            entity.Property(e => e.Correo)
                .HasMaxLength(25)
                .HasColumnName("correo");
            entity.Property(e => e.Direccion)
                .HasMaxLength(30)
                .HasColumnName("direccion");
            entity.Property(e => e.Distrito)
                .HasMaxLength(15)
                .HasColumnName("distrito");
            entity.Property(e => e.EstadoCliente)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("estado_cliente");
            entity.Property(e => e.FechaNacimiento).HasColumnName("fecha_nacimiento");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.Genero)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("genero");
            entity.Property(e => e.Nombre)
                .HasMaxLength(25)
                .HasColumnName("nombre");
            entity.Property(e => e.NumDocumento)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("num_documento");
            entity.Property(e => e.Telefono)
                .HasMaxLength(9)
                .IsUnicode(false)
                .HasColumnName("telefono");
            entity.Property(e => e.TipoDoc)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("tipo_doc");
        });

        modelBuilder.Entity<Concepto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Concepto__3214EC27B0AFAD19");

            entity.ToTable("Concepto");

            entity.Property(e => e.Id)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ID");
            entity.Property(e => e.Concepto1)
                .HasMaxLength(30)
                .HasColumnName("concepto");
        });

        modelBuilder.Entity<Diagnostico>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Diagnostico");

            entity.Property(e => e.Diagnostico1)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("diagnostico");
            entity.Property(e => e.Id)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ID");
            entity.Property(e => e.IdMedico)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ID_medico");

            entity.HasOne(d => d.Diagnostico1Navigation).WithMany()
                .HasForeignKey(d => d.Diagnostico1)
                .HasConstraintName("fk_diagnostico_tiposdiagnostico");

            entity.HasOne(d => d.IdNavigation).WithMany()
                .HasForeignKey(d => d.Id)
                .HasConstraintName("fk_diagnostico_registro");

            entity.HasOne(d => d.IdMedicoNavigation).WithMany()
                .HasForeignKey(d => d.IdMedico)
                .HasConstraintName("fk_diagnostico_empleado");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Empleado__3214EC27480861B1");

            entity.ToTable("Empleado");

            entity.Property(e => e.Id)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ID");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(20)
                .HasColumnName("apellidos");
            entity.Property(e => e.Correo)
                .HasMaxLength(25)
                .HasColumnName("correo");
            entity.Property(e => e.Direccion)
                .HasMaxLength(30)
                .HasColumnName("direccion");
            entity.Property(e => e.FechaNacimiento).HasColumnName("fecha_nacimiento");
            entity.Property(e => e.Genero)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("genero");
            entity.Property(e => e.Nombre)
                .HasMaxLength(25)
                .HasColumnName("nombre");
            entity.Property(e => e.NumDocumento)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("num_documento");
            entity.Property(e => e.Telefono)
                .HasMaxLength(9)
                .IsUnicode(false)
                .HasColumnName("telefono");
            entity.Property(e => e.TipoDoc)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("tipo_doc");
        });

        modelBuilder.Entity<Estetica>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Estetica");

            entity.Property(e => e.Concepto)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("concepto");
            entity.Property(e => e.Id)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ID");
            entity.Property(e => e.IdEmpleado)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ID_empleado");

            entity.HasOne(d => d.ConceptoNavigation).WithMany()
                .HasForeignKey(d => d.Concepto)
                .HasConstraintName("fk_estetica_concepto");

            entity.HasOne(d => d.IdNavigation).WithMany()
                .HasForeignKey(d => d.Id)
                .HasConstraintName("fk_estetica_registro");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany()
                .HasForeignKey(d => d.IdEmpleado)
                .HasConstraintName("fk_estetica_empleado");
        });

        modelBuilder.Entity<Paciente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Paciente__3214EC27F340C4CA");

            entity.ToTable("Paciente");

            entity.Property(e => e.Id)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ID");
            entity.Property(e => e.Altura).HasColumnName("altura");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(20)
                .HasColumnName("apellidos");
            entity.Property(e => e.Color)
                .HasMaxLength(10)
                .HasColumnName("color");
            entity.Property(e => e.Esterilizado).HasColumnName("esterilizado");
            entity.Property(e => e.FechaFallecimiento).HasColumnName("fecha_fallecimiento");
            entity.Property(e => e.FechaNacimiento).HasColumnName("fecha_nacimiento");
            entity.Property(e => e.GrupoSanguineo)
                .HasMaxLength(3)
                .IsFixedLength()
                .HasColumnName("grupo_sanguineo");
            entity.Property(e => e.IdTitular)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ID_titular");
            entity.Property(e => e.Longitud).HasColumnName("longitud");
            entity.Property(e => e.Microchip).HasColumnName("microchip");
            entity.Property(e => e.Morfologia)
                .HasMaxLength(15)
                .HasColumnName("morfologia");
            entity.Property(e => e.Nombre)
                .HasMaxLength(25)
                .HasColumnName("nombre");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(30)
                .HasColumnName("observaciones");
            entity.Property(e => e.Peso).HasColumnName("peso");
            entity.Property(e => e.Sexo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("sexo");
            entity.Property(e => e.Tatuaje).HasColumnName("tatuaje");
            entity.Property(e => e.Tipo)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("tipo");

            entity.HasOne(d => d.IdTitularNavigation).WithMany(p => p.Pacientes)
                .HasForeignKey(d => d.IdTitular)
                .HasConstraintName("fk_paciente_cliente");

            entity.HasOne(d => d.TipoNavigation).WithMany(p => p.Pacientes)
                .HasForeignKey(d => d.Tipo)
                .HasConstraintName("fk_paciente_tiposmascota");
        });

        modelBuilder.Entity<Registro>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Registro__3214EC275F267A61");

            entity.ToTable("Registro");

            entity.Property(e => e.Id)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ID");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("fecha");
            entity.Property(e => e.Hora)
                .HasPrecision(0)
                .HasDefaultValueSql("(CONVERT([time](0),getdate()))")
                .HasColumnName("hora");
            entity.Property(e => e.IdEmpleado)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ID_empleado");
            entity.Property(e => e.IdMascota)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ID_mascota");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.Registros)
                .HasForeignKey(d => d.IdEmpleado)
                .HasConstraintName("fk_registro_empleado");

            entity.HasOne(d => d.IdMascotaNavigation).WithMany(p => p.Registros)
                .HasForeignKey(d => d.IdMascota)
                .HasConstraintName("fk_registro_paciente");
        });

        modelBuilder.Entity<TiposDiagnostico>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tipos_Di__3214EC27C71C06E7");

            entity.ToTable("Tipos_Diagnostico");

            entity.Property(e => e.Id)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ID");
            entity.Property(e => e.Diagnostico)
                .HasMaxLength(50)
                .HasColumnName("diagnostico");
        });

        modelBuilder.Entity<TiposMascotum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tipos_Ma__3214EC27FA0CF5F5");

            entity.ToTable("Tipos_Mascota");

            entity.Property(e => e.Id)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ID");
            entity.Property(e => e.Nombre)
                .HasMaxLength(25)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<TiposVacuna>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tipos_Va__3214EC27A877BA6E");

            entity.ToTable("Tipos_Vacuna");

            entity.Property(e => e.Id)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ID");
            entity.Property(e => e.Vacuna)
                .HasMaxLength(30)
                .HasColumnName("vacuna");
        });

        modelBuilder.Entity<Vacuna>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.IdPaciente)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ID_paciente");
            entity.Property(e => e.IdVacuna)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ID_vacuna");

            entity.HasOne(d => d.IdPacienteNavigation).WithMany()
                .HasForeignKey(d => d.IdPaciente)
                .HasConstraintName("fk_vacunas_paciente");

            entity.HasOne(d => d.IdVacunaNavigation).WithMany()
                .HasForeignKey(d => d.IdVacuna)
                .HasConstraintName("fk_vacunas_tiposvacuna");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
