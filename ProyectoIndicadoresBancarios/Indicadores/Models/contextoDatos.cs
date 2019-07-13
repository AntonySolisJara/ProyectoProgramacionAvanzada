namespace Indicadores.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class contextoDatos : DbContext
    {
        public contextoDatos()
            : base("name=contextoDatos1")
        {
        }

        public virtual DbSet<CuentaCatalogo> CuentaCatalogo { get; set; }
        public virtual DbSet<Estado> Estado { get; set; }
        public virtual DbSet<Indicador> Indicador { get; set; }
        public virtual DbSet<IndicadorPosvalor> IndicadorPosvalor { get; set; }
        public virtual DbSet<Metadata> Metadata { get; set; }
        public virtual DbSet<Periodo> Periodo { get; set; }
        public virtual DbSet<Posvalor> Posvalor { get; set; }
        public virtual DbSet<UnidadMedida> UnidadMedida { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CuentaCatalogo>()
                .Property(e => e.CtaCatalogo)
                .IsUnicode(false);

            modelBuilder.Entity<CuentaCatalogo>()
                .HasMany(e => e.Indicador)
                .WithRequired(e => e.CuentaCatalogo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Estado>()
                .Property(e => e.SiglasEst)
                .IsUnicode(false);

            modelBuilder.Entity<Estado>()
                .Property(e => e.DescEstado)
                .IsUnicode(false);

            modelBuilder.Entity<Estado>()
                .HasMany(e => e.Indicador)
                .WithRequired(e => e.Estado)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Indicador>()
                .Property(e => e.CtaCatalogo)
                .IsUnicode(false);

            modelBuilder.Entity<Indicador>()
                .Property(e => e.SiglasEst)
                .IsUnicode(false);

            modelBuilder.Entity<Indicador>()
                .Property(e => e.SiglasPer)
                .IsUnicode(false);

            modelBuilder.Entity<Indicador>()
                .Property(e => e.SiglasUni)
                .IsUnicode(false);

            modelBuilder.Entity<Indicador>()
                .HasMany(e => e.IndicadorPosvalor)
                .WithRequired(e => e.Indicador)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Metadata>()
                .Property(e => e.SiglasMet)
                .IsUnicode(false);

            modelBuilder.Entity<Metadata>()
                .Property(e => e.NomMetadata)
                .IsUnicode(false);

            modelBuilder.Entity<Metadata>()
                .Property(e => e.DescMetadata)
                .IsUnicode(false);

            modelBuilder.Entity<Metadata>()
                .HasMany(e => e.Posvalor)
                .WithRequired(e => e.Metadata)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Periodo>()
                .Property(e => e.SiglasPer)
                .IsUnicode(false);

            modelBuilder.Entity<Periodo>()
                .Property(e => e.DescPeriodo)
                .IsUnicode(false);

            modelBuilder.Entity<Periodo>()
                .HasMany(e => e.Indicador)
                .WithRequired(e => e.Periodo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Posvalor>()
                .Property(e => e.SiglasMet)
                .IsUnicode(false);

            modelBuilder.Entity<Posvalor>()
                .Property(e => e.SiglasPos)
                .IsUnicode(false);

            modelBuilder.Entity<Posvalor>()
                .Property(e => e.NomPosvalor)
                .IsUnicode(false);

            modelBuilder.Entity<Posvalor>()
                .Property(e => e.DescPosvalor)
                .IsUnicode(false);

            modelBuilder.Entity<UnidadMedida>()
                .Property(e => e.SiglasUni)
                .IsUnicode(false);

            modelBuilder.Entity<UnidadMedida>()
                .Property(e => e.DescUnidad)
                .IsUnicode(false);

            modelBuilder.Entity<UnidadMedida>()
                .HasMany(e => e.Indicador)
                .WithRequired(e => e.UnidadMedida)
                .WillCascadeOnDelete(false);
        }
    }
}
