using Microsoft.EntityFrameworkCore;
using ScrumMasters.Webshop.Core.Models;
using ScrumMasters.Webshop.DataAccess.Entities;

namespace ScrumMasters.Webshop.DataAccess
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options): base(options)
        {
            
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<UserEntity>()
            //     .Property(u => u.Id)
            //     .ValueGeneratedNever();
        }

        public virtual DbSet<ProductEntity> Products { get; set; }
        public virtual DbSet<CategoryEntity> Categories { get; set; }
        public virtual DbSet<ColorEntity> Colors { get; set; }
        public virtual DbSet<SizeEntity> Sizes { get; set; }
        public virtual DbSet<ImageEntity> Images { get; set; }
        public virtual DbSet<UserEntity>  Users{ get; set; }
    }
}