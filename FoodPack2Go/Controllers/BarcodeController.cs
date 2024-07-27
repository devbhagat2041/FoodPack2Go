using FoodPack2Go.Core;
using IronBarCode;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace FoodPack2Go.UI.Controllers
{
    public class BarcodeController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        public BarcodeController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }



        
        
        public IActionResult CreateBarcode(int? id)
        {
            string _text=null;
            try
            {
                string barcode_text = ""; // Implement your captcha generation logic here
                Random random = new Random();
                for (int i = 0; i < 10; i++)
                {
                    barcode_text += random.Next(10).ToString();
                }
                
                GeneratedBarcode barcode = IronBarCode.BarcodeWriter.CreateBarcode(barcode_text, BarcodeWriterEncoding.Code128);
                barcode.ResizeTo(400, 120);
                barcode.AddBarcodeValueTextBelowBarcode();
                // Styling a Barcode and adding annotation text
                barcode.ChangeBarCodeColor(Color.Black);
                barcode.SetMargins(10);
                string path = Path.Combine(_environment.WebRootPath, "GeneratedBarcode");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string filePath = Path.Combine(_environment.WebRootPath, "GeneratedBarcode/barcode.png");
                barcode.SaveAsPng(filePath);
                string fileName = Path.GetFileName(filePath);
                string imageUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" + "/GeneratedBarcode/" + fileName;
                _text = imageUrl;
            }
            catch (Exception)
            {
                throw;
            }
            return RedirectToAction("Index","Order", new { text = _text ,orderid=id});
        }

    }
}
