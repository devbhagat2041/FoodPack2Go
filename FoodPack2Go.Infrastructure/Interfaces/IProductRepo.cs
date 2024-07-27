using FoodPack2Go.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPack2Go.Infrastructure.Interfaces
{
    public interface IProductRepo
    {
        IEnumerable<ProductModel> GetAll();
        Task<ProductModel> GetById(int id);
        void Add(ProductModel p);
        Task Update(ProductModel p);
        Task Delete(int id);
       

        IQueryable<ProductModel> Products { get; } // Add this property to represent the collection of products
        //  void ImportFromExcel(string filePath);
       
    }
}
