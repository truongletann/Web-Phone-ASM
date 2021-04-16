using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Model.sdsd
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<tblCategory> tblCategories { get; set; }
        public virtual DbSet<tblOrderDetail> tblOrderDetails { get; set; }
        public virtual DbSet<tblOrder> tblOrders { get; set; }
        public virtual DbSet<tblProduct> tblProducts { get; set; }
        public virtual DbSet<tblRole> tblRoles { get; set; }
        public virtual DbSet<tblUser> tblUsers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tblCategory>()
                .HasMany(e => e.tblProducts)
                .WithRequired(e => e.tblCategory)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblOrder>()
                .Property(e => e.userID)
                .IsUnicode(false);

            modelBuilder.Entity<tblOrder>()
                .HasMany(e => e.tblOrderDetails)
                .WithRequired(e => e.tblOrder)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblProduct>()
                .HasMany(e => e.tblOrderDetails)
                .WithRequired(e => e.tblProduct)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblRole>()
                .Property(e => e.roleID)
                .IsUnicode(false);

            modelBuilder.Entity<tblRole>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<tblUser>()
                .Property(e => e.userID)
                .IsUnicode(false);

            modelBuilder.Entity<tblUser>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<tblUser>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<tblUser>()
                .Property(e => e.roleID)
                .IsUnicode(false);

            modelBuilder.Entity<tblUser>()
                .HasMany(e => e.tblOrders)
                .WithRequired(e => e.tblUser)
                .WillCascadeOnDelete(false);
        }
    }
}
