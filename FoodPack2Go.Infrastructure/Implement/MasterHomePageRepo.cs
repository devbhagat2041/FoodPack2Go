using FoodPack2Go.Core;
using FoodPack2Go.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPack2Go.Infrastructure.Implement
{
    public class MasterHomePageRepo : IMasterHomePageRepo
    {

        private readonly MyAppDbContext app;

        public MasterHomePageRepo(MyAppDbContext _app)
        {
            app = _app;
        }

        public IEnumerable<MasterHomePage> GetAll()
        {
            return app.MasterHomePage.ToList();
        }

        public async Task<MasterHomePage> GetById(int id)
        {
            return await app.MasterHomePage.FindAsync(id);
        }
        public void Add(MasterHomePage b)
        {
            app.MasterHomePage.Add(b);
            app.SaveChanges();
        }
        public async Task Update(MasterHomePage b)
        {
            var h = await app.MasterHomePage.FindAsync(b.HomeId);
            if (h != null)
            {
                h.HomeId = b.HomeId;
                h.DescriptionOffer = b.DescriptionOffer;
                h.WebsiteTitle = b.WebsiteTitle;
                h.FeaturedProductTitle = b.FeaturedProductTitle;
                h.FeaturedProductDescription = b.FeaturedProductDescription;
                h.PromoTitle = b.PromoTitle;
                h.PromoTagline = b.PromoTagline;
                h.PromoDescription = b.PromoDescription;
                h.PromoProductname = b.PromoProductname;
                h.PromoOffertag = b.PromoOffertag;
                h.CompanyContactNo = b.CompanyContactNo;
                h.CompanyEmail = b.CompanyEmail;
                h.CompanyDescription = b.CompanyDescription;
                h.CompanyAddress = b.CompanyAddress;
                h.PromoImage = b.PromoImage;
                h.BgImage = b.BgImage;


                app.Update(h);
                app.SaveChanges();
            }
        }
        public async Task Delete(int id)
        {
            var h = await app.MasterHomePage.FindAsync(id);
            if (h != null)
            {
                app.MasterHomePage.Remove(h);
                app.SaveChanges();
            }
        }

        

       
    }
}
