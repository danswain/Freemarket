using Freemarket.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace Freemarket.Api.Data;

public class BasketDbContext : DbContext
{
    public DbSet<Basket> Baskets { get; set; }
    public DbSet<BasketItem> BasketItems { get; set; }
    
    public BasketDbContext(DbContextOptions<BasketDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Basket>()
            .HasMany(b => b.BasketItems)
            .WithOne()
            .HasForeignKey(i => i.BasketId);
    }
}