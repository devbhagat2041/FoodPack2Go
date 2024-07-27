using FoodPack2Go.Core;
using FoodPack2Go.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPack2Go.Infrastructure.Implement
{
    public class Repository : IRepository
    {
        private readonly MyAppDbContext app;

        public Repository(MyAppDbContext _app)
        {
            app = _app;
        }
        public AdminModel LoginAdmin(string Email, string Password)
        {
            var adn = app.Adminn.FirstOrDefault(c => c.Email == Email && c.Password == Password);
            if (adn != null)
            {
                adn.LastLogin = DateTime.UtcNow;
                app.SaveChangesAsync();
            }
            return adn;

        }
        public DateTime? GetLastLogin(string Email)
        {
            var admin = app.Adminn.FirstOrDefault(c => c.Email == Email);
            return admin?.LastLogin;
        }


        public int GetTotalCustomers()
        {
            return app.Customer.Count();
        }

        public int GetTotalProducts()
        {
            return app.Products.Count();
        }

        public int GetTotalOrders()
        {
            return app.Orders.Count();
        }
        public string OrderDistributionChart()
        {
            var categoryData = app.OrderDetails
                .GroupBy(od => od.Product.Category.CategoryName)
                .Select(group => new
                {
                    CategoryName = group.Key,
                    OrderCount = group.Count()
                })
                .ToList();

            // Transform the data into the format expected by Chart.js
            var chartData = new
            {
                labels = categoryData.Select(item => item.CategoryName),
                datasets = new[]
                {
            new
            {
                data = categoryData.Select(item => item.OrderCount),
                backgroundColor = new [] { "yellow", "black", "lightpink", "blue", "pink" },
                hoverBackgroundColor = new [] { "yellow", "black", "white", "blue", "pink" }
            }
        }
            };

            // Serialize the chartData object to JSON string
            return JsonConvert.SerializeObject(chartData);
        }

    }
}
