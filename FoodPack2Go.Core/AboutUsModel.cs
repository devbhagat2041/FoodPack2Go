






















using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPack2Go.Core
{
    public class AboutUsModel
    {
        [Key]
        public int AboutUsId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string? Title { get; set; }
        public string? Ashortdescription { get; set; }


        [Required(ErrorMessage = "Description is required")]
        public string? ADescription { get; set; }

        public string? ButtonText { get; set; }
        public int?  TotalCustomer { get; set; }
        public int? SoldProduct { get; set; }
        public int? Awards { get; set; }
        public string? AboutImage { get; set; }
        public string? AboutImage2 { get; set; }
    }
}
