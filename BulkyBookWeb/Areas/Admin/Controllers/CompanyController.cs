
using BulkyBook.DataAcess;
using BulkyBook.DataAcess.Repository;
using BulkyBook.DataAcess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBookWeb.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
           
        }

        public IActionResult Index()
        {
            return View();
        }

        //GET

        public IActionResult UpsertCategory(int? id)
        {
            Company company = new();
           

            if (id == 0 || id == null)
            {
                
                return View(company);
            }
            else
            {
                company = _unitOfWork.Company.GetFirstOrDefault(u => u.Id == id);
                return View(company);   
            }


        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpsertCategory(Company obj, IFormFile? file)
        {

            if (ModelState.IsValid)
            {
                
                if (obj.Id == 0)
                {
                    _unitOfWork.Company.Add(obj);
                    TempData["success"] = "Product  created sucessfully";
                }
                else
                {
                    _unitOfWork.Company.Update(obj);
                    TempData["success"] = "Product  updated sucessfully";
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            var companyList = _unitOfWork.Company.GetAll();
            return new JsonResult(new { data = companyList });
        }

        //POST
        [HttpDelete]
        public IActionResult DeleteCategory(int? id)
        {
            var obj = _unitOfWork.Company.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return new JsonResult(new { success = false, message = "Error while deleting" });

            }
          
            _unitOfWork.Company.Remove(obj);
            _unitOfWork.Save();


            return RedirectToAction("Index");
            //return new JsonResult(new { success = true, message = "Delete Successful" });

        }
        #endregion
    }

}
