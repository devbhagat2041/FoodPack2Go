

using ExcelDataReader;
using FoodPack2Go.Core;
using FoodPack2Go.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPack2Go.UI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepo db;
        private readonly ICategoryRepo categoryRepo;
        private readonly ISub subCategoryRepo;

        public ProductController(IProductRepo _db, ICategoryRepo _categoryRepo, ISub _subCategoryRepo)
        {
            db = _db;
            categoryRepo = _categoryRepo;
            subCategoryRepo = _subCategoryRepo;
        }

        public IActionResult Index()
        {
            var pro = db.GetAll();
            return View(pro);
        }

        [HttpGet]
       

        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(categoryRepo.GetAll(), "CategoryID", "CategoryName");
            ViewBag.SubCategories = new SelectList(Enumerable.Empty<SelectListItem>(), "Value", "Text");
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductModel p)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    p.CreatedBy = DateTime.Now;
                    db.Add(p);
                    TempData["successmessage"] = "Inserted!!!";

                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["errormessage"] = "Model is invalid";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["errormessage"] = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                ProductModel pro = await db.GetById(id);
                if (pro != null)
                {
                    ViewBag.Categories = new SelectList(categoryRepo.GetAll(), "CategoryID", "CategoryName", pro.CategoryID);
                    ViewBag.SubCategories = new SelectList(Enumerable.Empty<SelectListItem>(), "Value", "Text");
                    return View(pro);
                }
            }
            catch (Exception ex)
            {
                TempData["errormessage"] = ex.Message;
                return View();
            }

            TempData["errormessage"] = $"Not found with Id:{id}";
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> Edit(ProductModel p)
        {
            try
            {
                p.ModifiedBy = DateTime.Now;
                if (p.ImageURL == null)
                {

                    var product = await db.GetById(p.ProductID);
                    p.ImageURL = product.ImageURL;

                }
                await db.Update(p);
                TempData["successmessage"] = "Updated!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["errormessage"] = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                ProductModel pro = await db.GetById(id);
                if (pro != null)
                {
                    return View(pro);
                }
            }
            catch (Exception ex)
            {
                TempData["errormessage"] = ex.Message;
                return View();
            }

            TempData["errormessage"] = $"Not found with Id:{id}";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await db.Delete(id);
                TempData["successmessage"] = "Deleted!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["errormessage"] = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public IActionResult Import()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Import(IFormFile file)
        {
            try
            {

                if (file == null || file.Length == 0)
                {
                    TempData["errormessage"] = "Please select a file.";
                    return View();
                }

                // File Format Validation
                if (!file.FileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase))
                {
                    TempData["errormessage"] = "Invalid file format. Please select a valid Excel file.";
                    return View();
                }

                using (var stream = new MemoryStream())
                {
                    file.CopyTo(stream);
                    stream.Position = 0;

                    // Sheet Validation
                    using (var reader = ExcelDataReader.ExcelReaderFactory.CreateReader(stream))
                    {
                        var dataSet = reader.AsDataSet(new ExcelDataReader.ExcelDataSetConfiguration()
                        {
                            ConfigureDataTable = (_) => new ExcelDataReader.ExcelDataTableConfiguration()
                            {
                                UseHeaderRow = true
                            }
                        });

                        // Empty or Null Sheet Validation
                        if (dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
                        {
                            TempData["errormessage"] = "The Excel file is empty or does not contain valid data.";
                            return View();
                        }

                        // Iterate through each row in the Excel file
                        foreach (DataRow row in dataSet.Tables[0].Rows)
                        {
                            // Data Type Validation and Required Field Validation
                            if (string.IsNullOrWhiteSpace(row["ProductName"]?.ToString()) ||
                                string.IsNullOrWhiteSpace(row["CategoryID"]?.ToString()) ||
                                string.IsNullOrWhiteSpace(row["SubCategoryID"]?.ToString()) ||
                                string.IsNullOrWhiteSpace(row["QuantityPerUnit"]?.ToString()) ||
                                string.IsNullOrWhiteSpace(row["UnitPrice"]?.ToString()) ||
                                string.IsNullOrWhiteSpace(row["Capacity"]?.ToString()) ||
                                string.IsNullOrWhiteSpace(row["Size"]?.ToString()) ||
                                string.IsNullOrWhiteSpace(row["Material"]?.ToString()) ||
                                string.IsNullOrWhiteSpace(row["Discount"]?.ToString()) ||
                                string.IsNullOrWhiteSpace(row["UnitInStock"]?.ToString()) ||
                                string.IsNullOrWhiteSpace(row["ImageURL"]?.ToString()) ||
                                string.IsNullOrWhiteSpace(row["AltText"]?.ToString()) ||
                                string.IsNullOrWhiteSpace(row["Description"]?.ToString()))
                            {
                                TempData["errormessage"] = "One or more required fields are empty. Please check your Excel file.";
                                return View();
                            }

                            // Unique Key Validation (Assuming ProductName is a unique key)
                            var existingProduct = db.Products.FirstOrDefault(p => p.ProductName == row["ProductName"].ToString());
                            if (existingProduct != null)
                            {
                                TempData["errormessage"] = $"Product with name '{row["ProductName"].ToString()}' already exists. Duplicate entries are not allowed.";
                                return View();
                            }

                            // Data Type Conversion and Custom Business Rules Validation
                            if (!int.TryParse(row["CategoryID"].ToString(), out int categoryId) ||
                                !int.TryParse(row["SubCategoryID"].ToString(), out int subCategoryId) ||
                                !int.TryParse(row["QuantityPerUnit"].ToString(), out int quantityPerUnit) ||
                                !decimal.TryParse(row["UnitPrice"].ToString(), out decimal unitPrice) ||
                                !decimal.TryParse(row["Discount"].ToString(), out decimal discount) ||
                                !int.TryParse(row["UnitInStock"].ToString(), out int unitInStock))
                            {
                                TempData["errormessage"] = "Invalid data format in one or more columns. Please check your Excel file.";
                                return View();
                            }

                            // Range Validation
                            if (quantityPerUnit < 0 || unitPrice < 0 || discount < 0 || unitInStock < 0)
                            {
                                TempData["errormessage"] = "Invalid data range in one or more columns. Please check your Excel file.";
                                return View();
                            }

                            // Size Range Validation
                            if (!int.TryParse(row["Size"].ToString(), out int size) || size < 10 || size > 100)
                            {
                                TempData["errormessage"] = "Invalid size value. Please provide a size between 10 and 100.";
                                return View();
                            }

                            // Convert and map Excel row data to the ProductModel
                            var product = new ProductModel
                            {
                                ProductName = row["ProductName"].ToString(),
                                CategoryID = categoryId,
                                SubCategoryID = subCategoryId,
                                QuantityPerUnit = quantityPerUnit,
                                UnitPrice = unitPrice,
                                Capacity = row["Capacity"].ToString(),
                                Size = row["Size"].ToString(),
                                Material = row["Material"].ToString(),
                                Discount = discount,
                                UnitInStock = unitInStock,
                                ImageURL = row["ImageURL"].ToString(),
                                AltText = row["AltText"].ToString(),
                                Description = row["Description"].ToString(),
                                CreatedBy = DateTime.Now,
                                ModifiedBy = null,
                                IsDeleted = 0,
                                AverageRating = null
                            };

                            // Add the product to the database
                            db.Add(product);
                        }
                    }
                }

                TempData["successmessage"] = "Products imported successfully!";
                return RedirectToAction(nameof(Import));
            }
            catch (Exception ex)
            {
                TempData["errormessage"] = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public IActionResult GetSubCategories(int categoryId)
        {
            var subCategories = subCategoryRepo.GetAll().Where(s => s.CategoryID == categoryId);
            var selectList = new SelectList(subCategories, "SubCategoryID", "SubCategoryName");
            return Json(selectList);
        }

    }
}







//using ExcelDataReader;
//using FoodPack2Go.Core;
//using FoodPack2Go.Infrastructure.Interfaces;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;

//namespace FoodPack2Go.UI.Controllers
//{
//    public class ProductController : Controller
//    {
//        private readonly IProductRepo db;

//        public ProductController(IProductRepo _db)
//        {
//            db = _db;
//        }

//        public IActionResult Index()
//        {
//            var pro = db.GetAll();
//            return View(pro);
//        }

//        [HttpGet]
//        public IActionResult Create()
//        {
//            return View();
//        }

//        [HttpPost]
//        public IActionResult Create(ProductModel p)
//        {
//            try
//            {
//                if (ModelState.IsValid)
//                {
//                    p.CreatedBy = DateTime.Now;
//                    db.Add(p);
//                    return RedirectToAction("Index");
//                }
//                else
//                {
//                    TempData["errormessage"] = "Model is invalid";
//                    return View();
//                }
//            }
//            catch (Exception ex)
//            {
//                TempData["errormessage"] = ex.Message;
//                return View();
//            }
//        }



//        [HttpGet]
//        public async Task<IActionResult> Edit(int id)
//        {

//            var product = await db.GetById(id);
//            if (product == null)
//            {
//                return NotFound();
//            }

//            return View(product);
//        }

//        [HttpPost]
//        public async Task<IActionResult> Edit(ProductModel p)
//        {
//            try
//            {
//                p.ModifiedBy = DateTime.Now;
//                if(p.ImageURL==null)
//                {

//					var product = await db.GetById(p.ProductID);
//                    p.ImageURL = product.ImageURL;

//				}
//                await db.Update(p);
//                TempData["successmessage"] = "Updated!";
//                return RedirectToAction(nameof(Index));
//            }
//            catch (Exception ex)
//            {
//                TempData["errormessage"] = ex.Message;
//                return View();
//            }
//        }











































//        //DELETE
//        [HttpGet]
//        public async Task<IActionResult> Delete(int id)
//        {
//            try
//            {
//                ProductModel pro = await db.GetById(id)
//;
//                if (pro != null)
//                {
//                    return View(pro);
//                }
//            }
//            catch (Exception ex)
//            {
//                TempData["errormessage"] = ex.Message;
//                return View();
//            }

//            TempData["errormessage"] = $"Not found with Id:{id}";
//            return RedirectToAction(nameof(Index));
//        }

//        [HttpPost, ActionName("Delete")]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            try
//            {
//                await db.Delete(id)
//;
//                //TempData["successmessage"] = "Deleted!";
//                return RedirectToAction(nameof(Index));
//            }
//            catch (Exception ex)
//            {
//                TempData["errormessage"] = ex.Message;
//                return View();
//            }
//        }

//        [HttpGet]
//        public IActionResult Import()
//        {
//            return View();
//        }

//        [HttpPost]
//        public IActionResult Import(IFormFile file, List<IFormFile> Image)
//        {
//            try
//            {
//                if (file != null && file.Length > 0)
//                {
//                    // File Format Validation
//                    if (!file.FileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase))
//                    {
//                        TempData["errormessage"] = "Invalid file format. Please select a valid Excel file.";
//                        return View();
//                    }

//                    using (var stream = new MemoryStream())
//                    {
//                        file.CopyTo(stream);
//                        stream.Position = 0;

//                        // Sheet Validation
//                        using (var reader = ExcelReaderFactory.CreateReader(stream))
//                        {
//                            var dataSet = reader.AsDataSet(new ExcelDataSetConfiguration()
//                            {
//                                ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
//                                {
//                                    UseHeaderRow = true
//                                }
//                            });

//                            // Iterate through each row in the Excel file
//                            foreach (DataRow row in dataSet.Tables[0].Rows)
//                            {
//                                // Data Type Validation and Required Field Validation
//                                if (string.IsNullOrWhiteSpace(row["ProductName"]?.ToString()) ||
//                                    string.IsNullOrWhiteSpace(row["CategoryID"]?.ToString()) ||
//                                    string.IsNullOrWhiteSpace(row["SubCategoryID"]?.ToString()) ||
//                                    string.IsNullOrWhiteSpace(row["QuantityPerUnit"]?.ToString()) ||
//                                    string.IsNullOrWhiteSpace(row["UnitPrice"]?.ToString()) ||
//                                    string.IsNullOrWhiteSpace(row["Capacity"]?.ToString()) ||
//                                    string.IsNullOrWhiteSpace(row["Size"]?.ToString()) ||
//                                    string.IsNullOrWhiteSpace(row["Material"]?.ToString()) ||
//                                    string.IsNullOrWhiteSpace(row["Discount"]?.ToString()) ||
//                                    string.IsNullOrWhiteSpace(row["UnitInStock"]?.ToString()) ||
//                                    string.IsNullOrWhiteSpace(row["AltText"]?.ToString()) ||
//                                    string.IsNullOrWhiteSpace(row["Description"]?.ToString())
//                                     )
//                                {
//                                    TempData["errormessage"] = "One or more required fields are empty. Please check your Excel file.";
//                                    return View();
//                                }

//                                // Unique Key Validation (Assuming ProductName is a unique key)
//                                var existingProduct = db.Products.FirstOrDefault(p => p.ProductName == row["ProductName"].ToString());
//                                if (existingProduct != null)
//                                {
//                                    TempData["errormessage"] = $"Product with name '{row["ProductName"].ToString()}' already exists. Duplicate entries are not allowed.";
//                                    return View();
//                                }

//                                // Data Type Conversion and Custom Business Rules Validation
//                                if (!int.TryParse(row["CategoryID"].ToString(), out int categoryId) ||
//                                    !int.TryParse(row["SubCategoryID"].ToString(), out int subCategoryId) ||
//                                    !int.TryParse(row["QuantityPerUnit"].ToString(), out int quantityPerUnit) ||
//                                    !decimal.TryParse(row["UnitPrice"].ToString(), out decimal unitPrice) ||
//                                    !decimal.TryParse(row["Discount"].ToString(), out decimal discount) ||
//                                    !int.TryParse(row["UnitInStock"].ToString(), out int unitInStock))
//                                {
//                                    TempData["errormessage"] = "Invalid data format in one or more columns. Please check your Excel file.";
//                                    return View();
//                                }
//                                if (quantityPerUnit < 0 || unitPrice < 0 || discount < 0 || unitInStock < 0)
//                                {
//                                    TempData["errormessage"] = "Invalid data range in one or more columns. Please check your Excel file.";
//                                    return View();
//                                }
//                                if (!int.TryParse(row["Size"].ToString(), out int size) || size < 10 || size > 100)
//                                {
//                                    TempData["errormessage"] = "Invalid size value. Please provide a size between 10 and 100.";
//                                    return View();
//                                }
//                                string img=null;
//                                if (Image.First() != null && Image.First().Length > 0)
//                                {
//                                    // Generate a unique filename
//                                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + Image.First().FileName;

//                                    // Get the path where the image will be saved
//                                    var filePath = Path.Combine("wwwroot", "assets", "images", uniqueFileName);

//                                    // Save the image to the specified path
//                                    using (var fileStream = new FileStream(filePath, FileMode.Create))
//                                    {
//                                        Image.First().CopyTo(fileStream);
//                                    }

//                                    // Set the ImageURL property of the corresponding ProductModel object
//                                    img = "/assets/images/" + uniqueFileName; // Adjust the path as per your folder structure
//                                }
//                                // Convert and map Excel row data to the ProductModel
//                                var product = new ProductModel
//                                {
//                                    ProductName = row["ProductName"].ToString(),
//                                    CategoryID = categoryId,
//                                    SubCategoryID = subCategoryId,
//                                    QuantityPerUnit = quantityPerUnit,
//                                    UnitPrice = unitPrice,
//                                    Capacity = row["Capacity"].ToString(),
//                                    Size = row["Size"].ToString(),
//                                    Material = row["Material"].ToString(),
//                                    Discount = discount,
//                                    UnitInStock = unitInStock,
//                                    AltText = row["AltText"].ToString(),
//                                    Description = row["Description"].ToString(),
//                                    ImageURL=img,
//                                    CreatedBy = DateTime.Now,
//                                    ModifiedBy = null,
//                                    IsDeleted = 0,
//                                    AverageRating = null
//                                };

//                                // Add the product to the database
//                                db.Add(product);
//                            }

//                        }
//                    }

//                    TempData["successmessage"] = "Products imported successfully!";

//                    return RedirectToAction(nameof(Import));
//                }
//                else
//                {
//                    TempData["errormessage"] = "Please select a file to import.";
//                    return View();
//                }
//            }
//            catch (Exception ex)
//            {
//                TempData["errormessage"] = ex.Message;
//                return View();
//            }
//        }

//    }
//}





















