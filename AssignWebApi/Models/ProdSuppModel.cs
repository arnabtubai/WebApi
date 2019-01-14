namespace AssignWebApi.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ProdSuppModel : DbContext
    {
        public ProdSuppModel()
            : base("name=ProdSuppModel")
        {
        }

        public virtual DbSet<ITEM> ITEMs { get; set; }
        public virtual DbSet<PODETAIL> PODETAILs { get; set; }
        public virtual DbSet<POMASTER> POMASTERs { get; set; }
        public virtual DbSet<SUPPLIER> SUPPLIERs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ITEM>()
                .Property(e => e.ITCODE)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ITEM>()
                .Property(e => e.ITDESC)
                .IsUnicode(false);

            modelBuilder.Entity<ITEM>()
                .Property(e => e.ITRATE)
                .HasPrecision(19, 4);

            modelBuilder.Entity<ITEM>()
                .HasMany(e => e.PODETAILs)
                .WithRequired(e => e.ITEM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PODETAIL>()
                .Property(e => e.PONO)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<PODETAIL>()
                .Property(e => e.ITCODE)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<POMASTER>()
                .Property(e => e.PONO)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<POMASTER>()
                .Property(e => e.SUPLNO)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<POMASTER>()
                .HasMany(e => e.PODETAILs)
                .WithRequired(e => e.POMASTER)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SUPPLIER>()
                .Property(e => e.SUPLNO)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<SUPPLIER>()
                .Property(e => e.SUPLNAME)
                .IsUnicode(false);

            modelBuilder.Entity<SUPPLIER>()
                .Property(e => e.SUPLADDR)
                .IsUnicode(false);
        }

        public System.Data.Entity.DbSet<AssignWebApi.Models.SUPPLIERViewModel> SUPPLIERViewModels { get; set; }

        public System.Data.Entity.DbSet<AssignWebApi.Models.PODETAILViewModel> PODETAILViewModels { get; set; }

        public System.Data.Entity.DbSet<AssignWebApi.Models.ITEMViewModel> ITEMViewModels { get; set; }

        public System.Data.Entity.DbSet<AssignWebApi.Models.POMASTERViewModel> POMASTERViewModels { get; set; }
    }
}
