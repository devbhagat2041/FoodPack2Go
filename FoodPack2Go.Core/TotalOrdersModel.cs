using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPack2Go.Core
{
    
    public class TotalOrdersModel
    {
        public DateTime Date { get; set; }
        public int TotalOrders { get; set; }
        public string Week { get; set; }
        public string Month { get; set; }
        public int Year { get; set; }

        //public DateTime Date { get; set; }
        //public int OrderCount { get; set; }


    }
}
