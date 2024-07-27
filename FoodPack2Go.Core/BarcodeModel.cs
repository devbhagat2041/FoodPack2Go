using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPack2Go.Core
{
    public class BarcodeModel
    {
        [Display(Name = "Enter Barcode ")]
        public string BarcodeText { get; set; }
    }
}
