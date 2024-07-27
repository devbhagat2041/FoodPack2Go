using FoodPack2Go.Core;
using FoodPack2Go.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPack2Go.Infrastructure.Implement
{
    public class CategoylistRepository : ICategoylistRepository
    {
        private readonly MyAppDbContext app;

        public CategoylistRepository(MyAppDbContext _app)
        {
            this.app = _app;
        }

        public void Add(CategoryList s)
        {
            app.CategoryList.Add(s);
            app.SaveChanges();
        }

        public IEnumerable<CategoryList> GetAll()
        {
            return app.CategoryList.ToList();
        }

        public async Task<CategoryList> GetById(int id)
        {
            return await app.CategoryList.FindAsync(id);
        }

        public async Task Update(CategoryList s)
        {
            var sd = await app.CategoryList.FindAsync(s.CategoryListId);
            if (sd != null)
            {
                sd.CategoryListTitle = s.CategoryListTitle;
                sd.CategoryListImage = s.CategoryListImage;
                sd.CategoryListDescripton = s.CategoryListDescripton;
                sd.CategoryListButtonTitle = s.CategoryListButtonTitle;
                sd.CategoryListTitle2 = s.CategoryListTitle2;
                sd.CategoryListTitle3 = s.CategoryListTitle3;
                sd.CategoryListTitle4 = s.CategoryListTitle4;
                sd.CategoryListTitle5 = s.CategoryListTitle5;
                sd.CategoryListImage2 = s.CategoryListImage2;
                sd.CategoryListImage3 = s.CategoryListImage3;
                sd.CategoryListImage4 = s.CategoryListImage4;
                sd.CategoryListImage5 = s.CategoryListImage5;



                app.Update(sd);
                app.SaveChanges();
            }
        }

        public async Task Delete(int id)
        {
            var cat = await app.CategoryList.FindAsync(id);
            if (cat != null)
            {
                app.CategoryList.Remove(cat);
                app.SaveChanges();
            }
        }

    }
}
