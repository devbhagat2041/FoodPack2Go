using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPack2Go.Core
{
    public class Footer
    {
           
        [Key]
        public int FooterId { get; set; }
        public string? Privacy_Title { get; set; }

        public string? Privacy_Discription { get; set; }
        public string? Terms_Title { get; set; }
        public string? Terms_Discription { get; set; }
        public string? Return_Title { get; set; }
        public string? Return_Discription { get; set; }
    }
}

