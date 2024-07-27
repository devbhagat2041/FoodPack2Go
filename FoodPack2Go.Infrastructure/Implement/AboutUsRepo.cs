using FoodPack2Go.Core;
using FoodPack2Go.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPack2Go.Infrastructure.Implement
{
    public class AboutUsRepo : IAboutUsRepo
    {
        private readonly MyAppDbContext _app;

        public AboutUsRepo(MyAppDbContext app)
        {
            _app = app;
        }

        public IEnumerable<AboutUsModel> GetAll()
        {
            return _app.AboutUs.ToList();
        }

        public async Task<AboutUsModel> GetById(int id)
        {
            return await _app.AboutUs.FindAsync(id);
        }

        public void Add(AboutUsModel aboutUs)
        {
            _app.AboutUs.Add(aboutUs);
            _app.SaveChanges();
        }

        public async Task Update(AboutUsModel aboutUs)
        {
            var existingAboutUs = await _app.AboutUs.FindAsync(aboutUs.AboutUsId);

            if (existingAboutUs != null)
            {
                // Update properties
                existingAboutUs.Title = aboutUs.Title;
                existingAboutUs.Ashortdescription = aboutUs.Ashortdescription;
                existingAboutUs.ADescription = aboutUs.ADescription;
                existingAboutUs.ButtonText = aboutUs.ButtonText;
                existingAboutUs.TotalCustomer = aboutUs.TotalCustomer;
                existingAboutUs.SoldProduct = aboutUs.SoldProduct;
                existingAboutUs.Awards = aboutUs.Awards;
                existingAboutUs.AboutImage = aboutUs.AboutImage;
                existingAboutUs.AboutImage2 = aboutUs.AboutImage2;

                _app.Update(existingAboutUs);
                _app.SaveChanges();
            }
        }

        public async Task Delete(int id)
        {
            var aboutUsEntry = await _app.AboutUs.FindAsync(id);
            if (aboutUsEntry != null)
            {
                _app.AboutUs.Remove(aboutUsEntry);
                _app.SaveChanges();
            }
        }


    }
}
