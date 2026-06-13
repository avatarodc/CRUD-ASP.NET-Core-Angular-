using GestionProduits.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestionProduits.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Produit> Produits { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Produit>(entity =>
        {
            entity.ToTable("produits");
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Nom).HasMaxLength(100).IsRequired();
            entity.Property(p => p.Description).HasColumnType("text");
            entity.Property(p => p.Prix).HasColumnType("numeric(10,2)");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");
            entity.HasKey(u => u.Id);
            entity.Property(u => u.FirstName).HasMaxLength(100).IsRequired();
            entity.Property(u => u.LastName).HasMaxLength(100).IsRequired();
            entity.Property(u => u.Email).HasMaxLength(150).IsRequired();
            entity.HasIndex(u => u.Email).IsUnique();
            entity.Property(u => u.PasswordHash).IsRequired();
            entity.Property(u => u.Role).HasMaxLength(20).HasDefaultValue("User");
        });
    }
}
