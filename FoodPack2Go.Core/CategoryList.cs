using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPack2Go.Core
{
    public class CategoryList
    {
        [Key]
        public int CategoryListId { get; set; }
        public string? CategoryListTitle { get; set; }
        public string? CategoryListImage { get; set; }
        public string? CategoryListDescripton { get; set; }
        public string? CategoryListButtonTitle { get; set; }

        public string? CategoryListTitle2 { get; set; }
        public string? CategoryListTitle3 { get; set; }
        public string? CategoryListTitle4 { get; set; }
        public string? CategoryListTitle5 { get; set; }

        public string? CategoryListImage2 { get; set; }
        public string? CategoryListImage3 { get; set; }
        public string? CategoryListImage4 { get; set; }
        public string? CategoryListImage5 { get; set; }




    }
}
