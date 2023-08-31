using GrowUp.DataAccess.Repository.IRepository;
using GrowUp.Model;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GrowUpSite.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<PayMonthlyPlan> payMonthlyPlanEnumerator = _unitOfWork.PayMonthlyPlan.GetAll();
            return View(payMonthlyPlanEnumerator);
        }

        public IActionResult ContactUs()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ContactUs(Contact obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Contact.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = " Your Message Has Been successfully Sent ";
                return RedirectToAction("Index");
            }
          
            return View(obj);
        }

        public IActionResult Payment()
        {
            IEnumerable<PayMonthlyPlan> payMonthlyPlanEnumerator = _unitOfWork.PayMonthlyPlan.GetAll();
            return View(payMonthlyPlanEnumerator);
          
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [Route("StatusCodeError/{statusCode}")]
        public IActionResult Error(int statusCode)
        {
            switch (statusCode)
            {

                case 404:
                    ViewData["Error"] = "Page Not Found";
                    break;

                            case 505:
                    ViewData["Error"] = "Page Not Found";
                    break;
                default:
                    break;
            }

            return View("Error");

        }
    }
}