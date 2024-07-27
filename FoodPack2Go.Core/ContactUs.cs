using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPack2Go.Core
{
    public class ContactUs
    {
        [Key]
        public int ContactId { get; set; }
        public string? ContactImage { get; set; }
        public string? CustomerName { get; set; }
        public string? Email { get; set; }
        public string? CSubject { get; set; }
        public string? CMessage { get; set; }
        public string? ButtonText { get; set; }

        public string? CompanyAddress { get; set; }
        public string? CompanyContactNo { get; set; }
        public string? CompanyEmail { get; set; }


    }
}
