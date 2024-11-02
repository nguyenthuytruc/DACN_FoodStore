using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FoodStore.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Table> Tables { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<FoodCategory> FoodCategories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Payment> Payments { get; set; }

        public DbSet<Ingredients> Ingredients { get; set; }
        public DbSet<FoodIngredient> FoodIngredient { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FoodIngredient>()
                .HasKey(fi => new { fi.FoodId, fi.IngredientId }); // Khóa chính kết hợp

            modelBuilder.Entity<Food>()
               .Property(f => f.Price)
               .HasColumnType("decimal(18,2)"); // Thay đổi độ chính xác và quy mô theo nhu cầu của bạn
            modelBuilder.Entity<OrderDetail>()
                .HasKey(od => new { od.FoodId, od.OrderId });

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Food)
                .WithMany(f => f.OrderDetails)
                .HasForeignKey(od => od.FoodId);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId);

            base.OnModelCreating(modelBuilder);
        }
    }
}