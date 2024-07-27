using FoodPack2Go.Core;
using FoodPack2Go.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPack2Go.Infrastructure.Implement
{
    public class CategoryRepo:ICategoryRepo
    {
        private readonly MyAppDbContext app;

        public CategoryRepo(MyAppDbContext _app)
        {
            this.app = _app;
        }

        public IEnumerable<CategoryModel> GetAll()
        {
            return app.Category.ToList();
        }
       public void Addcategory(CategoryModel category)
        {
            app.Category.Add(category);
            app.SaveChanges();
        }

        public async Task<CategoryModel> GetById(int id)
        {
            return await app.Category.FindAsync(id);
        }

        public async Task Updatecategory(CategoryModel category)
        {
            var cat = await app.Category.FindAsync(category.CategoryID);
            if (cat != null)
            {
                cat.CategoryName = category.CategoryName;

                app.Update(cat);
                app.SaveChanges();
            }
        }

        public async Task Deletecategory(int id)
        {
            var cat = await app.Category.FindAsync(id);
            if (cat != null)
            {
                app.Category.Remove(cat);
                app.SaveChanges();
            }
        }

    }
}
