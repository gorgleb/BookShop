
using BulkyBook.DataAcess;
using BulkyBook.DataAcess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork=unitOfWork;
        }
       
        public IActionResult Index()
        {
            IEnumerable<CoverType> objCoverTypeList = _unitOfWork.CoverType.GetAll();
            return View(objCoverTypeList);
        }
        //GET
       
        public IActionResult CreateCategory()
        {
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCategory(CoverType obj)
        {
           
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Add(obj);
                _unitOfWork.Save();
                TempData["sucсess"] = "CoverType Created sucessfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        //GET

        public IActionResult EditCategory(int? id)
        {
            if (id==0||id==null)
            {
                return NotFound(); 
            }
            //var categoryFromDb= _db.Categories.Find(id);
            var CoverTypeFromDbFirst = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            if (CoverTypeFromDbFirst == null)
            {
                return NotFound();  
            }
            return View(CoverTypeFromDbFirst);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditCategory(CoverType obj)
        {
           
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "CoverType updated sucessfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public IActionResult DeleteCategory(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            var CoverTypeFromDbFirst = _unitOfWork.CoverType.GetFirstOrDefault(u=>u.Id==id);
            if (CoverTypeFromDbFirst == null)
            {
                return NotFound();
            }
            return View(CoverTypeFromDbFirst);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCategoryPost(int? id)
        {
            var obj = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            if (obj==null)
            {
                return NotFound(); 
            }
            _unitOfWork.CoverType.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "CoverType deleted sucessfully";
            return RedirectToAction("Index");
        
           
        }

    }
}
