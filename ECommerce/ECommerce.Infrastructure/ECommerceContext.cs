using Microsoft.EntityFrameworkCore;
using ECommerce.Infrastructure.Models;

namespace ECommerce.Infrastructure
{
    public class ECommerceContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<ProductStock> ProductStocks { get; set; }

        public ECommerceContext()
        {
            // Цей рядок гарантує, що база даних буде створена (для SQLite це зручно)
           
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=ecommerce.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductEntity>().UseTptMappingStrategy();

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<ProductEntity>()
                .HasOne(p => p.Stock)
                .WithOne(s => s.Product)
                .HasForeignKey<ProductStock>(s => s.ProductId);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Electronics" },
                new Category { Id = 2, Name = "Clothing" }
            );
        }
    }
}