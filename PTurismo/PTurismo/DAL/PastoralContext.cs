using PTurismo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace PTurismo.DAL
{
    public class PastoralContext : DbContext
    {
        public PastoralContext() : base("PastoralMySql")
        {

        }

        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Poi> Poi { get; set; }
        public DbSet<Elemento> Elemento { get; set; }
        public DbSet<GaleriaElemento> GaleriaElemento { get; set; }
        public DbSet<GaleriaPoi> GaleriaPoi { get; set; }
        public DbSet<FilePathPoi> FilePaths { get; set; }
        public DbSet<FilePathElemento> FilePathsEl { get; set; }
        public DbSet<FilePathCategoria> FilePathCats { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}