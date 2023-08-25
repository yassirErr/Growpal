using GrowUp.DataAccess.Repository.IRepository;
using GrowUp.Model;
using GrowUp.Model.ViewModels;
using GrowUp.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe.Checkout;
using System.Security.Claims;

namespace GrowUpSite.Areas.Customer.Controllers
{

    [Area("Customer")]

    public class SummaryController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public OrderVM OrderVM { get; set; }
        public SummaryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IActionResult Sumproved(int? id)
        {

            if (!User.Identity.IsAuthenticated)
            {
                // Redirect to the login page
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }

            if (id == null || id == 0)
            {
                return NotFound();
            }

            var paymentMothlyPlan = _unitOfWork.PayMonthlyPlan.GetFirstOrDefault(u => u.Id == id);
            if (paymentMothlyPlan == null) { return NotFound(); }

            return View(paymentMothlyPlan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Summary([FromForm] OrderVM orderVM ,int? id)
        {
            // Get the currently logged-in user ID
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            // Get the selected PayMonthlyPlan object from the database
            var selectedPlan = _unitOfWork.PayMonthlyPlan.GetFirstOrDefault(i => i.Id == id);
            orderVM.PayMonthlyPlan = new List<PayMonthlyPlan> { selectedPlan };

            // Create an instance of OrderHeader and set its properties
            var orderHeader = new OrderHeader
            {
                ApplicationUserId = claim.Value,
                OrderDate = DateTime.Now,
                OrderStatus = StaticDetail.StatusPending,
                PaymentStatus = StaticDetail.PaymentStatusPending,
                PaymentDate = DateTime.Now,
                PaymentDueDate = DateTime.Now.AddDays(7),
                OrderTotal = selectedPlan.PriceMonthly // Set the OrderTotal property
            };

            // Add the OrderHeader instance to the database
            _unitOfWork.OrderHeader.Add(orderHeader);

            // Create an instance of OrderDetail and set its properties
            var orderDetail = new OrderDetail
            {
                PayMonthlyPlan = selectedPlan, // Set the PayMonthlyPlan property
                Price = selectedPlan.PriceMonthly, // Set the Price property
                OrderHeader = orderHeader
            };

            // Add the OrderDetail instance to the database
            _unitOfWork.OrderDetail.Add(orderDetail);

            // Save the changes to the database
            _unitOfWork.Save();


            //stripe configuration


            var domain = "https://growpal.azurewebsites.net/";
            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>(),
              
                Mode = "payment",
                SuccessUrl = domain + $"customer/summary/OrderConfirmation?id={orderHeader.Id}",
                CancelUrl = domain + $"customer/home/Index",
            };

            foreach (var item in orderVM.PayMonthlyPlan)

            {
                var sessionLineItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        // Provide the exact Price ID (for example, pr_1234) of the product you want to sell
                        UnitAmount =(long)(item.PriceMonthly * 100),
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.SelectedOption,
                        },

                       
                    },
                    Quantity = 1,
                };
                options.LineItems.Add(sessionLineItem);
            };

            var service = new SessionService();
            Session session = service.Create(options);
            _unitOfWork.OrderHeader.UpdateStripePaymentID(orderHeader.Id , session.Id,session.PaymentIntentId);
            _unitOfWork.Save();
            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);

        }

        public IActionResult OrderConfirmation(int id)
        {
            OrderHeader orderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(u => u.Id == id, includeProperties: "ApplicationUser");
            var service = new SessionService();
            Session session = service.Get(orderHeader.SessionId);
            // checking the stripe status

            if (session.PaymentStatus.ToLower() == "paid")
            {
                _unitOfWork.OrderHeader.UpdateStripePaymentID(id, orderHeader.SessionId, session.PaymentIntentId);
                _unitOfWork.OrderHeader.UpdateStatus(id, StaticDetail.StatusPending, StaticDetail.PaymentStatusPending);
                _unitOfWork.Save();

            }
            _unitOfWork.Save();
            return View(id);

        }

    }
}
