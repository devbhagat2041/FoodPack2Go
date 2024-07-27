using FoodPack2Go.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPack2Go.Infrastructure.Interfaces
{
    public interface ISliderRepository
    {
        IEnumerable<Slider> GetAll();
        Task<Slider> GetById(int id);
        void Add(Slider s);
        Task Update(Slider s);
    }
}
