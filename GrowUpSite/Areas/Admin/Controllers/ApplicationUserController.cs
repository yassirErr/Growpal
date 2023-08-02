using GrowUp.DataAccess.Repository.IRepository;
using GrowUp.Model;
using GrowUp.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GrowUpSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =StaticDetail.Role_Admin)]
    public class ApplicationUserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ApplicationUserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<ApplicationUser> user = _unitOfWork.ApplicationUser.GetAll();

            List<string> userList = new List<string>();

            foreach (var item in user)
            {
                string country = _unitOfWork.Country.GetFirstOrDefault(c => c.id == item.CountyNameId)?.CountryName;
                string userListInfo = $"{country}";
                userList.Add(userListInfo);
            }

            ViewBag.UserList = userList;
            return View(user);
        }

        //GET
        public IActionResult Delete(string? id)
        {
            if (id == null || id.Equals(0))
            {
                return NotFound();
            }

            var ApplicationUserFormDbFirst = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == id);

            if (ApplicationUserFormDbFirst == null)
            {
                return NotFound();
            }

            return View(ApplicationUserFormDbFirst);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(string id)
        {
            // Get all Reactube records associated with the user
            var reactubes = _unitOfWork.Reactube.GetAll(r => r.ApplicationUserId == id);

            // Remove all Reactube records associated with the user from the repository
            _unitOfWork.Reactube.RemoveRange(reactubes);

            // Find the ApplicationUser record with the specified Id
            var user = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            // Remove the ApplicationUser record from the repository
            _unitOfWork.ApplicationUser.Remove(user);

            // Save the changes
            _unitOfWork.Save();

            TempData["success"] = "User deleted successfully";
            return RedirectToAction("Index");
        }


    }
}
