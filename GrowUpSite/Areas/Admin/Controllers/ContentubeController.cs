using GrowUp.DataAccess.Repository.IRepository;
using GrowUp.Model;
using GrowUp.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GrowUpSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = StaticDetail.Role_Admin)]
    public class ContentubeController : Controller
    {
       
        private readonly IUnitOfWork _unitOfWork;

        public ContentubeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Contentube> contentubeList = _unitOfWork.Content.GetAll();
            List<string> contentInfoList = new List<string>();

            foreach (var contentube in contentubeList)
            {
               
                string user = _unitOfWork.ApplicationUser.GetFirstOrDefault(a=>a.Id==contentube.ApplicationUserId)?.Name;
                string category = _unitOfWork.Category.GetFirstOrDefault(c => c.Id == contentube.Category_typeId)?.CategoryName;
                string service = _unitOfWork.Service.GetFirstOrDefault(s => s.Id == contentube.Service_typeId)?.ServiceName;
                string country = _unitOfWork.Country.GetFirstOrDefault(c => c.id == contentube.Country_nameId)?.CountryName;

                string contentInfo = $"{category} - {service} - {country}- {user}";
                contentInfoList.Add(contentInfo);
            }

            ViewBag.ContentInfoList = contentInfoList;
            return View(contentubeList);
        }
        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
           
            var contentFormDbFirst = _unitOfWork.Content.GetFirstOrDefault(u => u.Id == id);

            if (contentFormDbFirst == null)
            {
                return NotFound();
            }

            return View(contentFormDbFirst);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var contentFromDb = _unitOfWork.Content.GetFirstOrDefault(u => u.Id == id);

            if (contentFromDb == null)
            {
                return NotFound();
            }

            // Get all Watchtube records associated with the Content record
            var watchtubes = _unitOfWork.Watchtube.GetAll(w => w.ContentId == id);

            // Remove all Watchtube records associated with the Content record from the repository
            _unitOfWork.Watchtube.RemoveRange(watchtubes);

            // Remove the Content record from the repository
            _unitOfWork.Content.Remove(contentFromDb);

            // Save the changes
            _unitOfWork.Save();

            TempData["success"] = "Content Deleted successfully";
            return RedirectToAction("Index");
        }

        public IActionResult ContentRepo() 
        {
            IEnumerable<Contentube> contenRepo = _unitOfWork.Content.GetAll().Where(c=>c.StatusContent==false);
            return View(contenRepo); 

        }

        public ActionResult ChangeContentStatus(int? id)
        {
            var contentRepo = _unitOfWork.Content.GetFirstOrDefault(r => r.Id == id);

            if (contentRepo == null)
            {
                return NotFound();
            }

            contentRepo.StatusContent = !contentRepo.StatusContent; // toggle the status

            _unitOfWork.Content.Update(contentRepo);
            _unitOfWork.Save();


            return RedirectToAction("ContentRepo");

        }
    }
}
