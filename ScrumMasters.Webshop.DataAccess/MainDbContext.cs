using Microsoft.EntityFrameworkCore;
using ScrumMasters.Webshop.DataAccess.Entities;

namespace ScrumMasters.Webshop.DataAccess
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options): base(options)
        {
            
        }
        public virtual DbSet<ProductEntity> Products { get; set; }
    }
}