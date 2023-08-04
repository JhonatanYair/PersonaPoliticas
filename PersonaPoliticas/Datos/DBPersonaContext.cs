
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PersonaPoliticas.Datos.Configurations;
using PersonaPoliticas.Models;
using System;
using System.Collections.Generic;
namespace PersonaPoliticas.Datos
{
    public partial class DBPersonaContext : DbContext
    {
        public virtual DbSet<Genero> Genero { get; set; }
        public virtual DbSet<PersonaHijo> PersonaHijo { get; set; }
        public virtual DbSet<PersonaPadre> PersonaPadre { get; set; }

        public DBPersonaContext(DbContextOptions<DBPersonaContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Configurations.GeneroConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.PersonaHijoConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.PersonaPadreConfiguration());

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
