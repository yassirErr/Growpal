using GrowUp.DataAccess.Repository.IRepository;
using GrowUp.Model;
using GrowUp.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace GrowUpSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = StaticDetail.Role_Admin)]
    public class ServiceController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ServiceController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Service> objServiceList = _unitOfWork.Service.GetAll();
            return View(objServiceList);
        }
        //GET
        public IActionResult Create()
        {

            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Service obj)
        {
            if (obj.ServiceName == null)
            {
                ModelState.AddModelError("name", "Should Enter Value");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Service.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Service created successfully ";
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //var categoryFormDb = _db.Categories.Find(id);
            var ServiceFormDbFirst = _unitOfWork.Service.GetFirstOrDefault(u => u.Id == id);
            if (ServiceFormDbFirst == null)
            {
                return NotFound();
            }

            return View(ServiceFormDbFirst);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Service obj)
        {
            if (obj.ServiceName == null)
            {
                ModelState.AddModelError("name", "Should Enter Value");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Service.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Service Updated successfully ";
                return RedirectToAction("Index");
            }
            return View(obj);
        }



        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //  var categoryFormDb = _db.Categories.Find(id);
            var ServiceFormDbFirst = _unitOfWork.Service.GetFirstOrDefault(u => u.Id == id);

            if (ServiceFormDbFirst == null)
            {
                return NotFound();
            }

            return View(ServiceFormDbFirst);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            //var obj = _db.Categories.Find(id);
            var ServiceFormDbFirst = _unitOfWork.Service.GetFirstOrDefault(u => u.Id == id);
            if (ServiceFormDbFirst == null)
            {
                return NotFound();
            }

            _unitOfWork.Service.Remove(ServiceFormDbFirst);
            _unitOfWork.Save();
            TempData["success"] = "Service Deleted successfully";
            return RedirectToAction("Index");


        }
    }
}
