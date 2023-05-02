using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Models.Category> objCategoryList = _db.Categories;
            return View(objCategoryList);
        }
        //GET
        public IActionResult Create()
        {
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Name cannot match DisplayName");
            }
            if (!ModelState.IsValid)
            {
                return View(obj);
            }
            _db.Categories.Add(obj);
            _db.SaveChanges();
            TempData["success"] = "Successfully Added!";
            return RedirectToAction("Index");
        }
        //GET
        public IActionResult Edit(int? id)
        {
            if (id==null ||id<=0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Categories.Find(id);

            return View(categoryFromDb);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Name cannot match DisplayName");
            }
            if (!ModelState.IsValid)
            {
                return View(obj);
            }
            _db.Categories.Update(obj);
            _db.SaveChanges();
            TempData["success"] = "Successfully Updated!";
            return RedirectToAction("Index");
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int? id)
        {
            var obj = _db.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Successfully Deleted!";
            return RedirectToAction("Index");
        }
    }
}
