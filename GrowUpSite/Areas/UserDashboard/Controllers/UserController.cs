using GrowUp.DataAccess.Repository;
using GrowUp.DataAccess.Repository.IRepository;
using GrowUp.Model;
using GrowUp.Model.ViewModels;
using GrowUp.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Security.Claims;

namespace GrowUpSite.Areas.UserDashboard.Controllers
{

    [Area("UserDashboard")]
    [Authorize(Roles = StaticDetail.Role_User_Indi)]
    public class UserController : Controller
    {
        readonly private IUnitOfWork _db;

        public UserController(IUnitOfWork unitOfWork)
        {
            _db = unitOfWork;
        }

        private string GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
        public IActionResult Index()
        {

            string currentUserId = GetCurrentUserId();

            // Retrieve all data from the database and eagerly load the related entities
            IEnumerable<Contentube> contentubes = _db.Content.GetAll(includeProperties: "Category,Service");

            // Filter out the data for the currently logged in user
            contentubes = contentubes.Where(c => c.ApplicationUserId != currentUserId);

            // Pass the data to the view for display
            return View(contentubes);

        }


    }

    
}
