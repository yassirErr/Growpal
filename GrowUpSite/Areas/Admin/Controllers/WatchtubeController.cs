using GrowUp.DataAccess.Repository.IRepository;
using GrowUp.Model;
using GrowUp.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GrowUpSite.Areas.Admin.Controllers
{
   [Area("Admin")]
    [Authorize(Roles =StaticDetail.Role_Admin)]
    public class WatchtubeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public WatchtubeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
           var watchtubeList = _unitOfWork.Watchtube.GetAll();
            List<string> watchtubeInfo = new List<string>();

            foreach (var item in watchtubeList)
            {
                string content = _unitOfWork.Content.GetFirstOrDefault(c=>c.Id == item.ContentId)?.Content_name;
           
                string user = _unitOfWork.ApplicationUser.GetFirstOrDefault(c=>c.Id == item.ApplicationUserId)?.Name;

                string watchtubeListInfo = $"{content} - {user}";
                watchtubeInfo.Add(watchtubeListInfo);
            }
            ViewBag.WatchtubeInfo = watchtubeInfo;
            return View(watchtubeList);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var WatchtubetFromDb = _unitOfWork.Watchtube.GetFirstOrDefault(u => u.Id == id);

            if (WatchtubetFromDb == null)
            {
                return NotFound();
            }

            // Remove the Content record from the repository
            _unitOfWork.Watchtube.Remove(WatchtubetFromDb);

            // Save the changes
            _unitOfWork.Save();

            TempData["success"] = "Content Deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
