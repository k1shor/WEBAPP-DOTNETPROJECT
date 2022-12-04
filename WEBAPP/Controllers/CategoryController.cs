using Microsoft.AspNetCore.Mvc;
using WEBAPP.Data;
using WEBAPP.Models;
using WEBAPP.Repository.IRepository;

namespace WEBAPP.Controllers
{
    public class CategoryController : Controller
    {
        //private readonly ApplicationDbContext _db;
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            //var categoryObje = _db.Categories.ToList();
            IEnumerable<Category> obj = _unitOfWork.Category.GetAllData("");

            return View(obj);
        }
        //get
        public IActionResult Create()
        {
            return View();
        }

        //post
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category Added Successfully";
                return RedirectToAction("Index", "Category");
            }
            else
            {
                return View();
            }
        }

        public IActionResult Update(int? id)
        {
            if(id==null || id == 0)
            {
                return NotFound();
            }
            var category = _unitOfWork.Category.GetFirstOrDefault(u=>u.category_id == id);
            if (category != null)
            {
                return View(category);
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult Update(Category obj)
        {
            if (ModelState.IsValid)
            {
            _unitOfWork.Category.Update(obj);
            _unitOfWork.Save();
                TempData["success"] = "Category updated Successfully.";
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var category = _unitOfWork.Category.GetFirstOrDefault(u=>u.category_id == id);
            if (category != null)
            {
                return View(category);
            }
            return NotFound();
        }

        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            var category = _unitOfWork.Category.GetFirstOrDefault(u=>u.category_id == id);
            if (category != null)
            {
            _unitOfWork.Category.Remove(category);
                _unitOfWork.Save();
                TempData["success"] = "Category Deleted Successfully";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Failed to delete category";
                return View();
            }
            
        }

    }
}
