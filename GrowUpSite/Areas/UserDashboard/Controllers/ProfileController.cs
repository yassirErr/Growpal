using GrowUp.DataAccess.Repository.IRepository;
using GrowUp.Model;
using GrowUp.Utility;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GrowUpSite.Areas.UserDashboard.Controllers
{
    [Area("UserDashboard")]
    [Authorize(Roles = StaticDetail.Role_User_Indi)]
    public class ProfileController : Controller
    {
       
        public IActionResult EditUser()
        {

            return View();
        }
    }
}
