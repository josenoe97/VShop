using Microsoft.EntityFrameworkCore;
using VShop.ProductAPI.Models;

namespace VShop.ProductAPI.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        //Fluent API
    }
}
