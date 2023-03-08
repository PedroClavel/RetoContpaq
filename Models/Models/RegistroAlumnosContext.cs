using Microsoft.EntityFrameworkCore;

namespace Models.Models;

public partial class RegistroAlumnosContext : DbContext
{
    public RegistroAlumnosContext()
    {
    }

    public RegistroAlumnosContext(DbContextOptions<RegistroAlumnosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alumno> Alumnos { get; set; }

    public virtual DbSet<Sesion> Sesions { get; set; }

   // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=.;Database=RegistroAlumnos;User ID=sa;Password=r197355p; Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Alumno>(entity =>
        {
            entity.HasKey(e => e.IdAlumno).HasName("PK_idAlumno");

            entity.Property(e => e.IdAlumno).HasColumnName("idAlumno");
            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("apellidoMaterno");
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("apellidoPaterno");
            entity.Property(e => e.FechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaModificacion");
            entity.Property(e => e.Edad).HasColumnName("edad");
            entity.Property(e => e.FechaAlta)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaAlta");
            entity.Property(e => e.Grado).HasColumnName("grado");
            entity.Property(e => e.Grupo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("('')")
                .HasColumnName("grupo");
            entity.Property(e => e.Nombres)
                .IsUnicode(false)
                .HasColumnName("nombres");
            entity.Property(e => e.Telefono).HasColumnName("telefono");
        });

        modelBuilder.Entity<Sesion>(entity =>
        {
            entity.HasKey(e => e.IdLogin).HasName("PK_idLogin");

            entity.ToTable("Sesion");

            entity.Property(e => e.IdLogin).HasColumnName("idLogin");
            entity.Property(e => e.FechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaModificacion");
            entity.Property(e => e.FechaAlta)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaAlta");
            entity.Property(e => e.Pass)
                .IsUnicode(false)
                .HasColumnName("pass");
            entity.Property(e => e.Usuario)
                .IsUnicode(false)
                .HasColumnName("usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
