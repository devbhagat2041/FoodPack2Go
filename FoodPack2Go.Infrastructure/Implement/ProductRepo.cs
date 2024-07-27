using FoodPack2Go.Core;
using FoodPack2Go.Infrastructure.Interfaces;
using Microsoft.CodeAnalysis;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPack2Go.Infrastructure.Implement
{
    public class ProductRepo : IProductRepo

    {
        private readonly MyAppDbContext app;

        public ProductRepo(MyAppDbContext _app)
        {
            app = _app;
        }
        public IEnumerable<ProductModel> GetAll()
        {
            return app.Products.ToList();
        }

        public async Task<ProductModel> GetById(int id)
        {
            return await app.Products.FindAsync(id);
        }

        public void Add(ProductModel p)
        {
            //p.CreatedBy = DateTime.Now;
            app.Products.Add(p);

            app.SaveChanges();
        }

        public async Task Update(ProductModel p)
        {
            var pro = await app.Products.FindAsync(p.ProductID);
            if (pro != null)
            {
                pro.ProductName = p.ProductName;
                pro.CategoryID = p.CategoryID;
                pro.SubCategoryID = p.SubCategoryID;
                pro.QuantityPerUnit = p.QuantityPerUnit;
                pro.UnitPrice = p.UnitPrice;
                pro.Capacity = p.Capacity;
                pro.Size = p.Size;
                pro.Material = p.Material;
                pro.Discount = p.Discount;
                pro.UnitInStock = p.UnitInStock;
                pro.ImageURL = p.ImageURL;
                pro.AltText = p.AltText;
                pro.Description = p.Description;
                pro.CreatedBy = p.CreatedBy;
                pro.ModifiedBy = p.ModifiedBy;



                app.Update(pro);
                app.SaveChanges();
            }
        }


        public async Task Delete(int id)
        {
            var cat = await app.Products.FindAsync(id);
            if (cat != null)
            {
                app.Products.Remove(cat);
                app.SaveChanges();
            }
        }

      

         // Implement the Products property
        public IQueryable<ProductModel> Products => app.Products.AsQueryable();

       

    }
}
