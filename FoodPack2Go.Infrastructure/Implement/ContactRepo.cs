using FoodPack2Go.Core;
using FoodPack2Go.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPack2Go.Infrastructure.Implement
{
    public class ContactRepo : IContactRepo
    {
        private readonly MyAppDbContext app;

        public ContactRepo(MyAppDbContext _app)
        {
            this.app = _app;
        }

        public void Add(ContactUs c)
        {
            app.ContactUs.Add(c);
            app.SaveChanges();
        }

        public IEnumerable<ContactUs> GetAll()
        {
            return app.ContactUs.ToList();
        }

        public async Task<ContactUs> GetById(int id)
        {
            return await app.ContactUs.FindAsync(id);        }

        public async Task Update(ContactUs c)
        {
            var cu = await app.ContactUs.FindAsync(c.ContactId);
            if (cu != null)
            {
                cu.ContactImage = c.ContactImage;
                cu.CustomerName = c.CustomerName;
                c.Email = c.Email;
                cu.CSubject = c.CSubject;
                cu.CMessage= c.CMessage;
                cu.ButtonText = c.ButtonText;
                cu.CompanyAddress = c.CompanyAddress;
                cu.CompanyContactNo = c.CompanyContactNo;
                cu.CompanyEmail = c.CompanyEmail;

                app.Update(cu);
                app.SaveChanges();
            }
                
        }
    }
}
