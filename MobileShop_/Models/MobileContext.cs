using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MobileShop_.Models
{
    public partial class MobileContext : DbContext
    {
        public virtual DbSet<Catagory> Catagory { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Items> Items { get; set; }
        public virtual DbSet<Purchase> Purchase { get; set; }
        public virtual DbSet<Sales> Sales { get; set; }
        public virtual DbSet<UserProfile> UserProfile { get; set; }
        public virtual DbSet<Vender> Vender { get; set; }
		public MobileContext(DbContextOptions<MobileContext> abc) : base(abc)
		{

		}
		//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		//        {
		//            if (!optionsBuilder.IsConfigured)
		//            {
		//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
		//                optionsBuilder.UseSqlServer(@"Server=FahidSafdar62;Database=Mobile;Trusted_Connection=True; User ID=sa; Password=12345678;");
		//            }
		//        }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Catagory>(entity =>
            {
                entity.Property(e => e.CatagoryId).HasColumnName("CatagoryID");

                entity.Property(e => e.CatagoryName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.CustomerName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Items>(entity =>
            {
                entity.Property(e => e.ItemsId).HasColumnName("ItemsID");

                entity.Property(e => e.CatagoryId).HasColumnName("CatagoryID");

                entity.Property(e => e.Color)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Image)
                    .HasMaxLength(650)
                    .IsUnicode(false);

                entity.Property(e => e.ItemsName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Model)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Catagory)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.CatagoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Items_Catagory");
            });

            modelBuilder.Entity<Purchase>(entity =>
            {
                entity.Property(e => e.PurchaseId).HasColumnName("PurchaseID");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.ItemsId).HasColumnName("ItemsID");

                entity.Property(e => e.Pquanity).HasColumnName("PQuanity");

                entity.Property(e => e.VenderId).HasColumnName("VenderID");

                entity.HasOne(d => d.Items)
                    .WithMany(p => p.Purchase)
                    .HasForeignKey(d => d.ItemsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Purchase_Items");

                entity.HasOne(d => d.Vender)
                    .WithMany(p => p.Purchase)
                    .HasForeignKey(d => d.VenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Purchase_Vender");
            });

            modelBuilder.Entity<Sales>(entity =>
            {
                entity.Property(e => e.SalesId).HasColumnName("SalesID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.ItemsId).HasColumnName("ItemsID");

                entity.Property(e => e.TrDate).HasColumnType("date");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sales_Customers");

                entity.HasOne(d => d.Items)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(d => d.ItemsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sales_Items");
            });

            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserRole)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Vender>(entity =>
            {
                entity.HasKey(e => e.Vid);

                entity.Property(e => e.Vid).HasColumnName("VID");

                entity.Property(e => e.Vmobile).HasColumnName("VMobile");

                entity.Property(e => e.Vname)
                    .HasColumnName("VName")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
