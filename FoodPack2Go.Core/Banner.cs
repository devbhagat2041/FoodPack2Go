using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPack2Go.Core
{
    public class Banner
    {
        [Key]
        public int BannerId { get; set; }

        public string? BannerImage { get; set; }

        public string? BannerTitle { get; set; }

        public string? BannerDescription { get; set; }
    }

}
