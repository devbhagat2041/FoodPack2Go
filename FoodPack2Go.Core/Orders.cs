using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPack2Go.Core
{
    public class Orders
    {
        [Key]
        public int OrderID { get; set; }
        public int? CustomerID { get; set; }
        public string? TotalAmount { get; set; }
        public string? Discount { get; set; }
        public string? SubTotal { get; set; }
        public string? OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }

        public Customer? Customer { get; set; }
        //[Display(Name = "Enter Barcode ")]
        //public string BarcodeText { get; set; }

        [NotMapped]
        public string BarcodeText { get; set; }

        [NotMapped]
        public DateTime OrderDateWithoutTime => OrderDate.Date;

    }
}
