using GrowUp.DataAccess.Repository.IRepository;
using GrowUp.Model;
using GrowUp.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GrowUpSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = StaticDetail.Role_Admin)]
    public class ReactubeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReactubeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Reactube> objReactubeList = _unitOfWork.Reactube.GetAll();
            List<string>  listReactube = new List<string>();

            foreach (var item in objReactubeList)
            {
                string user = _unitOfWork.ApplicationUser.GetFirstOrDefault(a => a.Id == item.ApplicationUserId)?.Name;
                string content = _unitOfWork.Content.GetFirstOrDefault(c=>c.Id == item.ContentId)?.Content_name;

                string contentIfon = $"{content} - {user}";
                listReactube.Add(contentIfon);
            }
            ViewBag.ListReactube = listReactube;
            return View(objReactubeList);
        }


        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var reactubeFormDbFirst = _unitOfWork.Reactube.GetFirstOrDefault(u => u.Id == id);

            if (reactubeFormDbFirst == null)
            {
                return NotFound();
            }

            return View(reactubeFormDbFirst);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var contentFromDb = _unitOfWork.Reactube.GetFirstOrDefault(u => u.Id == id);

            if (contentFromDb == null)
            {
                return NotFound();
            }

            // Get all Watchtube records associated with the Content record
            var watchtubes = _unitOfWork.Watchtube.GetAll(w => w.ReactubeId == id);

            // Remove all Watchtube records associated with the Content record from the repository
            _unitOfWork.Watchtube.RemoveRange(watchtubes);

            // Remove the Content record from the repository
            _unitOfWork.Reactube.Remove(contentFromDb);

            // Save the changes
            _unitOfWork.Save();

            TempData["success"] = "Content Deleted successfully";
            return RedirectToAction("Index");
        }



    }


}
