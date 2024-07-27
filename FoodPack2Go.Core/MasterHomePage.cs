using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FoodPack2Go.Core
{
    public class MasterHomePage
    {
        [Key]
        public int HomeId { get; set; }
        public string? DescriptionOffer { get; set; }
        public string? WebsiteTitle { get; set; }
        public string? FeaturedProductTitle { get; set; }
        public string? FeaturedProductDescription { get; set; }
        public string? PromoTitle { get; set; }
        public string? PromoTagline { get; set; }
        public string? PromoDescription { get; set; }
        public string? PromoProductname { get; set; }
        public string? PromoOffertag { get; set; }

        public string?  CompanyContactNo { get; set; }
        public string? CompanyEmail { get; set; }
        public string? CompanyDescription { get; set; }
        public string? CompanyAddress { get; set; }
        public string? PromoImage { get; set; }

        public string? BgImage { get; set; }

    }
}
