using FoodPack2Go.Core;
using FoodPack2Go.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPack2Go.Infrastructure.Implement
{
    public class BannerRepository : IBannerRepository
    {
        private readonly MyAppDbContext app;

        public BannerRepository(MyAppDbContext _app)
        {
            this.app = _app;
        }
        public IEnumerable<Banner> GetAll()
        {
            return app.Banner.ToList();

        }

        public async Task<Banner> GetById(int id)
        {
            return await app.Banner.FindAsync(id);
        }

        public void Add(Banner b)
        {

            app.Banner.Add(b);
            app.SaveChanges();
        }

        public async Task Update(Banner b)
        {
            var sd = await app.Banner.FindAsync(b.BannerId);
            if (sd != null)
            {
                sd.BannerTitle = b.BannerTitle;
                sd.BannerImage = b.BannerImage;
                sd.BannerDescription = b.BannerDescription;

                app.Update(sd);
                app.SaveChanges();
            }
        }
        public async Task Delete(int id)
        {
            var cat = await app.Banner.FindAsync(id);
            if (cat != null)
            {
                app.Banner.Remove(cat);
                app.SaveChanges();
            }
        }

    }
}
