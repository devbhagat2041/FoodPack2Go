using FoodPack2Go.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPack2Go.Infrastructure.Interfaces
{
    public interface IBannerRepository
    {
        IEnumerable<Banner> GetAll();
        Task<Banner> GetById(int id);
        void Add(Banner b);
        Task Update(Banner b);
        Task Delete(int id);
    }
}
