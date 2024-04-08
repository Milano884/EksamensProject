using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ZBC.Models;

public partial class ZBCDBContext : DbContext
{
    public ZBCDBContext()
    {
    }

    public ZBCDBContext(DbContextOptions<ZBCDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Article> Articles { get; set; }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<Laptop> Laptops { get; set; }

    public virtual DbSet<Maker> Makers { get; set; }

    public virtual DbSet<Pc> Pcs { get; set; }

    public virtual DbSet<Printer> Printers { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<TestTable> TestTables { get; set; }

    public virtual DbSet<VwLaptopB> VwLaptopBs { get; set; }

    public virtual DbSet<VwPcB> VwPcBs { get; set; }

    public virtual DbSet<VwPrinterB> VwPrinterBs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Article>(entity =>
        {
            entity.Property(e => e.ArticleId).HasColumnName("ArticleID");
            entity.Property(e => e.ArticleCategory).HasMaxLength(64);
            entity.Property(e => e.ArticleSummary).HasMaxLength(512);
            entity.Property(e => e.ArticleTitle).HasMaxLength(256);
            entity.Property(e => e.PublicationDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.ProviderKey).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.Name).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Laptop>(entity =>
        {
            entity.HasKey(e => e.ModelId).HasName("PK__Laptop__E8D7A1CC54E066B6");

            entity.ToTable("Laptop");

            entity.Property(e => e.ModelId)
                .ValueGeneratedNever()
                .HasColumnName("ModelID");
            entity.Property(e => e.Screen).HasColumnType("decimal(3, 1)");

            entity.HasOne(d => d.Model).WithOne(p => p.Laptop)
                .HasForeignKey<Laptop>(d => d.ModelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Laptop_Product");
        });

        modelBuilder.Entity<Maker>(entity =>
        {
            entity.ToTable("Maker");

            entity.Property(e => e.MakerId)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MakerID");
            entity.Property(e => e.MakerColor)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        modelBuilder.Entity<Pc>(entity =>
        {
            entity.HasKey(e => e.ModelId).HasName("PK__Pc__E8D7A1CC25A2FE96");

            entity.ToTable("Pc");

            entity.Property(e => e.ModelId)
                .ValueGeneratedNever()
                .HasColumnName("ModelID");
            entity.Property(e => e.ReadDrive)
                .HasMaxLength(16)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.Model).WithOne(p => p.Pc)
                .HasForeignKey<Pc>(d => d.ModelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pc_Product");
        });

        modelBuilder.Entity<Printer>(entity =>
        {
            entity.HasKey(e => e.ModelId).HasName("PK__Printer__E8D7A1CC39394E20");

            entity.ToTable("Printer");

            entity.Property(e => e.ModelId)
                .ValueGeneratedNever()
                .HasColumnName("ModelID");
            entity.Property(e => e.Color)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.PrinterType)
                .HasMaxLength(16)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.Model).WithOne(p => p.Printer)
                .HasForeignKey<Printer>(d => d.ModelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Printer_Product");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ModelId).HasName("PK__Product__E8D7A1CC5CDDDD60");

            entity.ToTable("Product");

            entity.Property(e => e.ModelId)
                .ValueGeneratedNever()
                .HasColumnName("ModelID");
            entity.Property(e => e.MakerId)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MakerID");
            entity.Property(e => e.ProductType)
                .HasMaxLength(16)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.Maker).WithMany(p => p.Products)
                .HasForeignKey(d => d.MakerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_Maker");
        });

        modelBuilder.Entity<TestTable>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<VwLaptopB>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Vw_Laptop_B");

            entity.Property(e => e.ModelId).HasColumnName("ModelID");
        });

        modelBuilder.Entity<VwPcB>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Vw_Pc_B");

            entity.Property(e => e.ModelId).HasColumnName("ModelID");
        });

        modelBuilder.Entity<VwPrinterB>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Vw_Printer_B");

            entity.Property(e => e.ModelId).HasColumnName("ModelID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
