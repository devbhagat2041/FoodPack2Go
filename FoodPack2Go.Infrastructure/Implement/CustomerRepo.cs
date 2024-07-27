using FoodPack2Go.Core;
using FoodPack2Go.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPack2Go.Infrastructure.Implement
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly MyAppDbContext app;

        public CustomerRepo(MyAppDbContext _app)
        {
            app = _app;
        }

        public  IEnumerable<Customer> GetAll()
        {
            return  app.Customer.ToList();
                
        }

        public async Task<Customer> GetById(int id)
        {
            return await app.Customer.FindAsync(id);        }
        public void Add(Customer p)
        {
            app.Customer.Add(p);
            app.SaveChanges();
        }


        public async Task Update(Customer p)
        {
            var c = await app.Customer.FindAsync(p.CustomerID);

            if(c != null)
            {
                c.FirstName = p.FirstName;
                c.LastName = p.LastName;
                c.Address = p.Address;
                c.Pincode = p.Pincode;  
                c.Country = p.Country;
                c.State = p.State;
                c.City = p.City;
                c.ContactNo = p.ContactNo;
                c.CreatedBy = p.CreatedBy;
                c.ModifiedBy = p.ModifiedBy;
                c.LastLogin = p.LastLogin;

                app.Customer.Update(c);
                app.SaveChanges();
            }
        }

        public async Task Delete(int id)
        {
            var c = await app.Customer.FindAsync(id);
            if( c != null)
            {
                app.Customer.Remove(c);
                app.SaveChanges();
            }
        }

        public int GetTotalCustomersAsync()
        {
            return app.Customer.Count();
        }
    }
}
