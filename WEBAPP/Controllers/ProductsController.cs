using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WEBAPP.Data;
using WEBAPP.Models;
using WEBAPP.Repository;
using WEBAPP.Repository.IRepository;
using static System.Net.Mime.MediaTypeNames;

namespace WEBAPP.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _iwhe;
        public ProductsController(IUnitOfWork unitOfWork, IWebHostEnvironment iwhe)
        {
            _unitOfWork = unitOfWork;
            _iwhe = iwhe;

        }

        // GET: Products
        public IActionResult Index()
        {
            IEnumerable<Product> productlist = _unitOfWork.Product.GetAllData("Category");
            return View(productlist);
        }
        [Authorize]
        public IActionResult Upsert(int? id)
        {
            Product product = new();
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAllData("").Select(
                u => new SelectListItem
                {
                    Text = u.category_name,
                    Value = u.category_id.ToString()
                }
            );
            if(id == 0 || id == null)
            {
                // create product
                ViewBag.categoryList = CategoryList;
                return View(product);
            }
            else
            {
                ViewBag.categoryList = CategoryList;
                Product productfromdb = _unitOfWork.Product.GetFirstOrDefault(u => u.product_id == id);
            return View(productfromdb);
                // update product
            }
    }
        [HttpPost]
        [Authorize]
        public IActionResult Upsert(Product obj, IFormFile? file)
        {

            if (ModelState.IsValid)
            {
                string path = _iwhe.WebRootPath;

                if (file != null)
                {
                    if (obj.product_image_link != null)
                    {
                        var oldImageLink = Path.Combine(path, @"Images\Products\" + obj.product_image_link);
                        if (System.IO.File.Exists(oldImageLink))
                        {
                            System.IO.File.Delete(oldImageLink);
                        }
                    }
                string filename = Guid.NewGuid().ToString();
                var uploads = Path.Combine(path, @"Images\Products");
                var extension = Path.GetExtension(file.FileName);
                using (var filestreams = new FileStream(Path.Combine(uploads, filename + extension), FileMode.Create))
                {
                    file.CopyTo(filestreams);
                }
                obj.product_image_link = filename + extension;

                }
                



                if (obj.product_id == 0)
                {
                    obj.rating = 1;
                    obj.createdAt = DateTime.Now;
                    _unitOfWork.Product.Add(obj);
                    TempData["success"] = "New Product Added";
                }
                else
                {
                    _unitOfWork.Product.Update(obj);
                    TempData["success"] = "Product Updated";
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");

            }
            else
            {
                return View(obj);
            }
        }

        #region API
        [HttpGet]
        public IActionResult Getall()
        {
            var productlist = _unitOfWork.Product.GetAllData("Categories");
            return Json(new { data = productlist });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _unitOfWork.Product.GetFirstOrDefault(u => u.product_id == id);
            if(obj == null)
            {
                return Json(new {success= false, message= "Error while deleting"});
            }
            string path = _iwhe.WebRootPath;
            if (obj.product_image_link != null)
            {
                var oldImageLink = Path.Combine(path, @"Images\Products\" + obj.product_image_link);
                if (System.IO.File.Exists(oldImageLink))
                {
                    System.IO.File.Delete(oldImageLink);
                }
            }
            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Product deleted Successfully" });
        }
        #endregion



    }
}
