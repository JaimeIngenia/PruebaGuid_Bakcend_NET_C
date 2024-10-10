using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DBPruebaGuid.Models
{
    public partial class DBPruebaGuidContext : DbContext
    {
        public DBPruebaGuidContext()
        {
        }

        public DBPruebaGuidContext(DbContextOptions<DBPruebaGuidContext> options)
            : base(options)
        {
            this.Database.EnsureCreated();
        }

        public virtual DbSet<Categoria> Categorias { get; set; } = null!;
        public virtual DbSet<Producto> Productos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Configura tu conexión aquí si no lo haces en Program.cs
                // optionsBuilder.UseSqlServer("Connection string aquí");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de la entidad 'Categoria'
            modelBuilder.Entity<Categoria>(entity =>
            {
                // Indica que no se debe generar automáticamente el GUID
                entity.Property(e => e.Id).ValueGeneratedNever();

                // Configura las propiedades
                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsRequired();
            });

            // Configuración de la entidad 'Producto'
            modelBuilder.Entity<Producto>(entity =>
            {
                // Indica que no se debe generar automáticamente el GUID
                entity.Property(e => e.Id).ValueGeneratedNever();

                // Configura las propiedades
                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.Precio)
                    .HasColumnType("decimal(18, 2)");

                // Configura la relación con 'Categoria'
                entity.HasOne(d => d.Categoria)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.CategoriaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Producto_Categoria");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
