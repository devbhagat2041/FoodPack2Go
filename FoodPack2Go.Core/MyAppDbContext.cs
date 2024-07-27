using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPack2Go.Core
{
    public class MyAppDbContext : DbContext
    {
        public MyAppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<AdminModel> Adminn { get; set; }
        public DbSet<CategoryModel> Category { get; set; }
        public DbSet<SubModel> SubCategory { get; set; }
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<Slider> Slider { get; set; }
        public DbSet<CategoryList> CategoryList { get; set; }
        public DbSet<Banner> Banner { get; set; }
        public DbSet<MasterHomePage> MasterHomePage { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Footer> FooterInfo { get; set; }
        public DbSet<AboutUsModel> AboutUs { get; set; }

        public DbSet<Cart> Cart { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Coupon> Coupon { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<Payment> Payment { get; set; }







        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Cascading delete from CategoryModel to SubModel
            modelBuilder.Entity<CategoryModel>()
                .HasMany(c => c.SubCategories)
                .WithOne(s => s.Category)
                .HasForeignKey(s => s.CategoryID)
                .OnDelete(DeleteBehavior.Cascade);

            // Cascading delete from SubModel to ProductModel
            modelBuilder.Entity<SubModel>()
                .HasMany(s => s.Products)
                .WithOne(p => p.SubCategory)
                .HasForeignKey(p => p.SubCategoryID)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}

