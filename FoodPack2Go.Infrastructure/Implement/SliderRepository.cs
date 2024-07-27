using FoodPack2Go.Core;
using FoodPack2Go.Infrastructure.Interfaces;
using NuGet.Versioning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPack2Go.Infrastructure.Implement
{
    public class SliderRepository : ISliderRepository
    {
        private readonly MyAppDbContext app;

        public SliderRepository(MyAppDbContext _app)
        {
            this.app = _app;
        }

        public void Add(Slider s)
        {
            app.Slider.Add(s);
            app.SaveChanges();
        }

        public IEnumerable<Slider> GetAll()
        {
            return app.Slider.ToList();
        }

        public async Task<Slider> GetById(int id)
        {
            return await app.Slider.FindAsync(id);
        }

        public async Task Update(Slider s)
        {
            var sd = await app.Slider.FindAsync(s.SliderId);
            if(sd != null)
            {
                sd.SliderImage= s.SliderImage;
                sd.SliderTitle= s.SliderTitle;
                sd.SliderDescription= s.SliderDescription;
                sd.SliderButtonTitle= s.SliderButtonTitle;

                app.Update(sd);
                app.SaveChanges();
            }
        }
    }
}
