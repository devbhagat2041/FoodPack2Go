using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPack2Go.Core
{
    public class Slider
    {
        [Key]
        public int SliderId { get; set; }
        public string? SliderImage { get; set; }
        public string? SliderTitle { get; set; }
        public string? SliderDescription { get; set; }
        public string? SliderButtonTitle { get; set; }


    }
}
