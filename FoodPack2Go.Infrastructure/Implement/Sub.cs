using FoodPack2Go.Core;
using FoodPack2Go.Infrastructure.Interfaces;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPack2Go.Infrastructure.Implement
{
    public class Sub : ISub
    {
        private readonly MyAppDbContext app;

        public Sub(MyAppDbContext _app)
        {
            app = _app;
        }

        public IEnumerable<SubModel> GetAll()
        {
            return app.SubCategory.ToList();
        }
       
        public void Add(SubModel s)
        {
            app.Add(s);
            app.SaveChanges();
        }

        public async Task Update(SubModel s)
        {
            var cat = await app.SubCategory.FindAsync(s.SubCategoryID);
            if (cat != null)
            {
                cat.CategoryID = s.CategoryID;
                cat.SubCategoryName = s.SubCategoryName;


                app.Update(cat);
                app.SaveChanges();
            }
        }

        public async Task Delete(int id)
        {
            var cat = await app.SubCategory.FindAsync(id);
            if (cat != null)
            {
                app.SubCategory.Remove(cat);
                app.SaveChanges();
            }
        }

        public void Save()
        {
            app.SaveChanges();
        }

        public async Task<SubModel> GetById(int id)
        {
            return await app.SubCategory.FindAsync(id);

        }
    }
}
