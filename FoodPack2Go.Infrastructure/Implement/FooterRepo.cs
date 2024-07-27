using FoodPack2Go.Core;
using FoodPack2Go.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPack2Go.Infrastructure.Implement
{
    public class FooterRepo : IFooterRepo
    {
        private readonly MyAppDbContext app;

        public FooterRepo(MyAppDbContext _app)
        {
            this.app = _app;
        }

        public void Add(Footer footer)
        {
            app.FooterInfo.Add(footer);
            app.SaveChanges();
        }

        public IEnumerable<Footer> GetAll()
        {
            return app.FooterInfo.ToList();
        }

        public async Task<Footer> GetById(int id)
        {
            return await app.FooterInfo.FindAsync(id);        }


        async Task IFooterRepo.Update(Footer footer)
        {
            var ft = await app.FooterInfo.FindAsync(footer.FooterId);
            if (ft != null) 
            {
                ft.Privacy_Title = footer.Privacy_Title;
                ft.Privacy_Discription = footer.Privacy_Discription;
                ft.Terms_Title = footer.Terms_Title;
                ft.Terms_Discription = footer.Terms_Discription;
                ft.Return_Title = footer.Return_Title;
                ft.Return_Discription = footer.Return_Discription;

                app.FooterInfo.Update(ft);
                app.SaveChanges();
            }
        }
    }
}
