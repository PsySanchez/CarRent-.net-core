using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CarRent.DAL.Models
{
    public partial class CarRentContext : DbContext
    {
        public CarRentContext()
        {
        }

        public CarRentContext(DbContextOptions<CarRentContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cars> Cars { get; set; }
        public virtual DbSet<CompanyFleet> CompanyFleet { get; set; }
        public virtual DbSet<ContactUsMessage> ContactUsMessage { get; set; }
        public virtual DbSet<ManufacturersCar> ManufacturersCar { get; set; }
        public virtual DbSet<ModelsCar> ModelsCar { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Tokens> Tokens { get; set; }
        public virtual DbSet<UserOrders> UserOrders { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=HP-Lap;Initial Catalog=CarRent;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cars>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Cars");

                entity.Property(e => e.CarNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Image)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.ManufacturerName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Model)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PricePerDay).HasColumnType("money");
            });

            modelBuilder.Entity<CompanyFleet>(entity =>
            {
                entity.Property(e => e.CarNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Model)
                    .WithMany(p => p.CompanyFleet)
                    .HasForeignKey(d => d.ModelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompanyFleet_ModelsCar");
            });

            modelBuilder.Entity<ContactUsMessage>(entity =>
            {
                entity.HasKey(e => e.ContactUsId);

                entity.Property(e => e.ContactUsId).HasColumnName("ContactUsID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Subject)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<ManufacturersCar>(entity =>
            {
                entity.Property(e => e.ManufacturerName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ModelsCar>(entity =>
            {
                entity.Property(e => e.Image)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Model)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PricePerDay).HasColumnType("money");

                entity.HasOne(d => d.Manufacturer)
                    .WithMany(p => p.ModelsCar)
                    .HasForeignKey(d => d.ManufacturerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ModelsCar_ManufacturersCar");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.Property(e => e.FromDate).HasColumnType("date");

                entity.Property(e => e.ToDate).HasColumnType("date");

                entity.Property(e => e.TotalCost).HasColumnType("money");

                entity.HasOne(d => d.Car)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CarId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_CompanyFleet");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_Orders");
            });

            modelBuilder.Entity<Tokens>(entity =>
            {
                entity.Property(e => e.LifeTime).HasColumnType("datetime");

                entity.Property(e => e.Token).HasMaxLength(50);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Tokens)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tokens_Users");
            });

            modelBuilder.Entity<UserOrders>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("UserOrders");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FromDate).HasColumnType("date");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ManufacturerName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Model)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Photo)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.ToDate).HasColumnType("date");

                entity.Property(e => e.TotalCost).HasColumnType("money");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasIndex(e => e.Email)
                    .HasName("Email")
                    .IsUnique();

                entity.HasIndex(e => e.Id)
                    .HasName("IX_Users");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
