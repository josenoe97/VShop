using Microsoft.EntityFrameworkCore;
using VShop.ProductAPI.Models;

namespace VShop.ProductAPI.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        //Fluent API => sobreescrita para não vir a sobrecarga padrão do entityFramework
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //category
            modelBuilder.Entity<Category>().HasKey(c => c.CategoryId);

            modelBuilder.Entity<Category>().
                Property(c => c.Name).
                    HasMaxLength(100).
                        IsRequired();

            //product
            modelBuilder.Entity<Product>().
                Property(c => c.Name).
                    HasMaxLength(100).
                        IsRequired();

            modelBuilder.Entity<Product>().
               Property(c => c.Description).
                   HasMaxLength(255).
                       IsRequired();

            modelBuilder.Entity<Product>().
               Property(c => c.ImagemURL).
                   HasMaxLength(255).
                       IsRequired();

            modelBuilder.Entity<Product>().
               Property(c => c.Price).
                   HasPrecision(12, 2);

            //1:N
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                    .WithOne(c => c.Category)
                        .IsRequired()
                            .OnDelete(DeleteBehavior.Cascade); // DeleteBehavior.Cascade => na exclusão de uma categoria os produtos relacionados irão ser excluidos


            //HasData => se não houver dados iniciais irá inserir
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    CategoryId = 1,
                    Name = "Material Escolar",
                },

                new Category
                {
                    CategoryId = 2,
                    Name = "Acessórios"
                }
            );
        }

    }
}
