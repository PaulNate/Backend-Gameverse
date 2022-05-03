using Microsoft.EntityFrameworkCore;
using Gameverse.Models;

namespace Gameverse.Data;

public class GameverseContext : DbContext
{
    public GameverseContext(DbContextOptions<GameverseContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<ShoppingCart> ShoppingCarts => Set<ShoppingCart>();
    public DbSet<Review> Reviews => Set<Review>();
    //public DbSet<ProductShoppingCart> ProductShoppingCarts { get; set; }
    public DbSet<Voucher> Vouchers => Set<Voucher>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductShoppingCart>()
            .HasKey(bc => new { bc.ProductId, bc.ShoppingCartId });
        modelBuilder.Entity<ProductShoppingCart>()
            .HasOne(bc => bc.Product)
            .WithMany(b => b.ProductShoppingCarts)
            .HasForeignKey(bc => bc.ProductId);
        // modelBuilder.Entity<ProductShoppingCart>()
        //     .HasOne(bc => bc.ShoppingCart)
        //     .WithMany(c => c.ProductShoppingCarts)
        //     .HasForeignKey(bc => bc.ShoppingCartId);
        modelBuilder.Entity<ShoppingCart>()
            .HasOne(b => b.User)
            .WithMany(b => b.shoppingCart)
            .HasForeignKey(b => b.UserId);
    }
}