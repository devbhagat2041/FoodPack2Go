using FoodPack2Go.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPack2Go.Infrastructure.Interfaces
{
    public interface IAboutUsRepo
    {
        IEnumerable<AboutUsModel> GetAll();
        Task<AboutUsModel> GetById(int id);
        void Add(AboutUsModel aboutUs);
        Task Update(AboutUsModel aboutUs);
        Task Delete(int id);
    }
}
