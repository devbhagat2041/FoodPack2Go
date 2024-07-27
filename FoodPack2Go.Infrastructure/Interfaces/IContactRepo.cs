using FoodPack2Go.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPack2Go.Infrastructure.Interfaces
{
    public interface IContactRepo
    {
        IEnumerable<ContactUs> GetAll();
        Task<ContactUs> GetById(int id);
        void Add(ContactUs c);
        Task Update(ContactUs c);
    }
}
