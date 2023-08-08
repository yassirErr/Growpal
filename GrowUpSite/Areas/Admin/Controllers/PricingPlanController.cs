using GrowUp.DataAccess.Repository.IRepository;
using GrowUp.Model;
using GrowUp.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GrowUpSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =StaticDetail.Role_Admin)]

    public class PricingPlanController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public PricingPlanController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IActionResult Index()
        {

          IEnumerable<PayMonthlyPlan> PayMonthList = _unitOfWork.PayMonthlyPlan.GetAll();
            return View(PayMonthList);
        }



        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PayMonthlyPlan obj)
        {
            //if (obj.PriceMonthly==0)
            //{
            //    ModelState.AddModelError("Information", "Should Enter Value");
            //}

            if (ModelState.IsValid)
            {
                _unitOfWork.PayMonthlyPlan.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "PayMonthlyPlan created successfully ";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id ==0)
            {
                return NotFound();
            }
            var PayMonthlyPlanFromDb = _unitOfWork.PayMonthlyPlan.GetFirstOrDefault(i=>i.Id == id);

            if (PayMonthlyPlanFromDb == null)
            {
                return NotFound();
            }

            return View(PayMonthlyPlanFromDb);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PayMonthlyPlan obj)
        {
          if(obj.PriceMonthly == null)
            {
                ModelState.AddModelError("PriceMonthly", "Should Enter Value");
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.PayMonthlyPlan.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "PayMonthlyPlan Updated successfully ";
                return RedirectToAction("Index");
            }

            return View(obj);
        }


        public IActionResult Delete(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            var payMonthly = _unitOfWork.PayMonthlyPlan.GetFirstOrDefault(p=>p.Id == id);

            if (payMonthly == null)
            {
                return NotFound();
            }
            return View(payMonthly);

        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {

            var payMonthly = _unitOfWork.PayMonthlyPlan.GetFirstOrDefault(p => p.Id == id);

            if (payMonthly == null)
            {
                return NotFound();
            }
                _unitOfWork.PayMonthlyPlan.Remove(payMonthly);
                _unitOfWork.Save();
                TempData["success"] = "Category Deleted successfully";
                return RedirectToAction("Index");

        }


    }
}
