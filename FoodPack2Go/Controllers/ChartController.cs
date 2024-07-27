using Microsoft.AspNetCore.Mvc;
using FoodPack2Go.Core; // Adjust the namespace based on your project structure
using System.Linq;

public class ChartController : Controller
{
    private readonly MyAppDbContext _dbContext; // Replace YourDbContext with your actual DbContext class

    public ChartController(MyAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IActionResult OrderDistributionChart()
    {
        var categoryData = _dbContext.OrderDetails
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
                    backgroundColor = new [] { "black", "yellow", "orange", "lightgreen", "red" },
                    hoverBackgroundColor = new [] { "blue", "yellow", "orange", "lightgreen", "red" }
                }
            }
        };
        
        return View(chartData);
    }
}
