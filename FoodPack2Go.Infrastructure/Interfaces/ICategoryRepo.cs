using FoodPack2Go.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPack2Go.Infrastructure.Interfaces
{
    public interface ICategoryRepo
    {
        IEnumerable<CategoryModel> GetAll();
        Task<CategoryModel> GetById(int id);
        void Addcategory(CategoryModel category);
        Task Updatecategory(CategoryModel category);
        Task Deletecategory(int id);
    }
}
