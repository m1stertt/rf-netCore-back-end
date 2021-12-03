using Microsoft.EntityFrameworkCore;
using ScrumMasters.Webshop.DataAccess.Entities;
using ScrumMasters.Webshop.Security.Model;

namespace ScrumMasters.Webshop.DataAccess
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options): base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductCategories>()
                .HasKey(u => new {u.CategoryId, u.ProductId});
        }

        public virtual DbSet<ProductEntity> Products { get; set; }
        public virtual DbSet<CategoryEntity> Categories { get; set; }
        
        public DbSet<ProductCategories> ProductCategories { get; set; }
    }
}