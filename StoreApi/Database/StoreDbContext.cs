using Microsoft.EntityFrameworkCore;
using StoreApi.Database.Entity;

namespace StoreApi.Database
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
