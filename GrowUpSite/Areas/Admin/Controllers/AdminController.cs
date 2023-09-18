using GrowUp.DataAccess.Repository.IRepository;
using GrowUp.Model;
using GrowUp.Model.ViewModels;
using GrowUp.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using static GrowUpSite.Areas.Identity.Pages.Account.Manage.ChangePasswordModel;

namespace GrowUpSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = StaticDetail.Role_Admin)]
    public class AdminController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<AdminController> _logger;
        public AdminController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ILogger<AdminController> logger)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Profile()
        {

            // get the ID of the current user
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //var userApp = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == userId);

            ApplicationUserVM ApplicationUserVM = new()
            {
                ApplicationUser = new(),
                CountryListItem = _unitOfWork.Country.GetAll().Select
                (
                    u => new SelectListItem
                    {
                        Text = u.CountryName,
                        Value = u.id.ToString(),
                    }
                 )
            };

            if (userId == null)
            {

                return NotFound();
            }

            ApplicationUserVM.ApplicationUser=_unitOfWork.ApplicationUser.GetFirstOrDefault(a=>a.Id == userId.ToString());
            return View(ApplicationUserVM);
        }


        //public async Task<IActionResult> PasswordUpdate()
        //{
        //    var user = await _userManager.GetUserAsync(User);
        //    if (user == null)
        //    {
        //        return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        //    }

        //    var hasPassword = await _userManager.HasPasswordAsync(user);

        //    if (!hasPassword)
        //    {
        //        return RedirectToPage("./SetPassword");
        //    }

        //    return Profile();
        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> PasswordUpdate([FromForm] InputModel input) 
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Profile();
        //    }

        //    var user = await _userManager.GetUserAsync(User);

        //    if (user == null)
        //    {
        //        return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        //    }
        //    var changePasswordResult = await _userManager.ChangePasswordAsync(user, input.OldPassword, input.NewPassword);
        //    if (!changePasswordResult.Succeeded)
        //    {
        //        foreach (var error in changePasswordResult.Errors)
        //        {
        //            ModelState.AddModelError(string.Empty, error.Description);
        //        }
        //        return Profile();
        //    }
        //    // Sign the user out
        //    await _signInManager.SignOutAsync();

        //    _logger.LogInformation("User changed their password successfully.");

        //    // Redirect the user to the login page
        //    return RedirectToPage("/Account/Login", new { returnUrl = "~/" });
        //}

    }
}
