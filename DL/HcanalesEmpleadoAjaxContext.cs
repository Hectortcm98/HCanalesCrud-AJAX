using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DL;

public partial class HcanalesEmpleadoAjaxContext : DbContext
{
    public HcanalesEmpleadoAjaxContext()
    {
    }


    public HcanalesEmpleadoAjaxContext(DbContextOptions<HcanalesEmpleadoAjaxContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<EntidadFederativa> EntidadFederativas { get; set; }

   
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.; Database= HCanalesEmpleadoAJAX; TrustServerCertificate=True; User ID=sa; Password=pass@word1;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ////Lo agrege para los triggers
        modelBuilder.Entity<Empleado>()
            .ToTable(tb => tb.HasTrigger("trg_GenerarNumeroNomina"));

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado).HasName("PK__Empleado__CE6D8B9EA963CA9D");

            entity.ToTable("Empleado");

            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NumeroNomina)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdEstado)
                .HasConstraintName("FK__Empleado__IdEsta__1BFD2C07");
          
        });

        modelBuilder.Entity<EntidadFederativa>(entity =>
        {
            entity.HasKey(e => e.IdEstado).HasName("PK__EntidadF__FBB0EDC10F2A0E19");

            entity.ToTable("EntidadFederativa");

            entity.Property(e => e.Estado)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);

      
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    

}
