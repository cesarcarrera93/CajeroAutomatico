using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace CajeroAutomatico.Models
{
    public class CajeroAutomaticoDbContext : DbContext
    {
        public DbSet<Tarjeta> Tarjetas { get; set; }
        public DbSet<Operacion> Operaciones { get; set; }
        public DbSet<TipoOperacion> TipoOperacion { get; set; }

        public CajeroAutomaticoDbContext() : base("MiCajeroAutomatico")
        {
        }
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<Tarjeta>()
        //        .HasMany(t => t.Operaciones)
        //        .WithRequired(o => o.Tarjeta)
        //        .HasForeignKey(o => o.NroTarjeta);

        //    modelBuilder.Entity<TipoOperacion>()
        //        .HasMany(t => t.Operaciones)
        //        .WithRequired(o => o.TipoOperacion)
        //        .HasForeignKey(o => o.IdTipoOperacion);
        //}
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new TarjetaConfiguration());
            modelBuilder.Configurations.Add(new OperacionConfiguration());
            modelBuilder.Configurations.Add(new TipoOperacionConfiguration());
        }

        public class TarjetaConfiguration : EntityTypeConfiguration<Tarjeta>
        {
            public TarjetaConfiguration()
            {
                ToTable("Tarjetas");
                HasKey(t => t.NroTarjeta);
                Property(t => t.NroTarjeta).HasMaxLength(16);
                Property(t => t.Pin).HasMaxLength(4);
            }
        }

        public class OperacionConfiguration : EntityTypeConfiguration<Operacion>
        {
            public OperacionConfiguration()
            {
                ToTable("Operaciones");
                HasKey(o => o.CodigoOperacion);
                Property(o => o.NroTarjeta).HasMaxLength(16);
                //HasRequired(o => o.Tarjeta)
                //    .WithMany(t => t.Operaciones)
                //    .HasForeignKey(o => o.NroTarjeta)
                //    .WillCascadeOnDelete(false);
                //HasRequired(o => o.TipoOperacion)
                //    .WithMany()
                //    .HasForeignKey(o => o.IdTipoOperacion)
                //    .WillCascadeOnDelete(false);
            }
        }

        public class TipoOperacionConfiguration : EntityTypeConfiguration<TipoOperacion>
        {
            public TipoOperacionConfiguration()
            {
                ToTable("TipoOperacion");
                HasKey(t => t.IdTipoOperacion);
                Property(t => t.Operacion).HasMaxLength(50);
            }
        }
    }
}