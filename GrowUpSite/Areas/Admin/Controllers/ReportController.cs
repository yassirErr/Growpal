using GrowUp.DataAccess.Repository.IRepository;
using GrowUp.Model;
using GrowUp.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GrowUpSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =StaticDetail.Role_Admin)]
    public class ReportController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReportController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        private DateTime? startTime = null;
        private string GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        private int CountData()
        {
            string currentUserId = GetCurrentUserId();
            int countWatchtube = _unitOfWork.Watchtube.Count(w => w.ApplicationUserId == currentUserId);
            return countWatchtube;
        }

        private int CountInsertedByOthers()
        {
            string currentUserId = GetCurrentUserId();
            int insertedByOthersCount = 0;

            // Get the Category_typeId and Service_typeId of the current user's Contentube record
            int categoryTypeId = _unitOfWork.Content.GetAll(c => c.ApplicationUserId == currentUserId).Select(c => c.Category_typeId).FirstOrDefault();
            int serviceTypeId = _unitOfWork.Content.GetAll(c => c.ApplicationUserId == currentUserId).Select(c => c.Service_typeId).FirstOrDefault();

            // Get the Watchtube records where ItemVideo is not the current user but the ApplicationUser is the current user
            // Get the VideoLink items inserted by other users for the current user's Contentube record
            var videoLinksInsertedByOthers = _unitOfWork.Watchtube.GetAll(
                w => w.Content.Category_typeId == categoryTypeId &&
                w.Content.Service_typeId == serviceTypeId &&
                w.Reactube.ItemVideo != currentUserId &&
                w.Content.ApplicationUserId == currentUserId
            ).Select(w => w.VideoLink);

            // Count the number of Watchtube records
            insertedByOthersCount = videoLinksInsertedByOthers.Count();

            return insertedByOthersCount;
        }

        private bool IsInsertedByOthersCountGreaterThanWatchtubeCount()
        {
            int insertedByOthersCount = CountInsertedByOthers();
            int watchtubeCount = CountData();

            // Check if the condition is met
            if (insertedByOthersCount > watchtubeCount)
            {
                if (startTime == null)
                {
                    // Set the start time when the condition is first met
                    startTime = DateTime.Now;
                }
            }
            else
            {
                // Reset the start time if the condition is not met
                startTime = null;
            }

            return true;
        }

        public IActionResult ContentRepo()
        {

            IEnumerable<Contentube> contenttubeRepo = _unitOfWork.Content.GetAll();
            // Check if the condition is met
            if (IsInsertedByOthersCountGreaterThanWatchtubeCount())
            {
                // Check if 24 hours have passed since the start time
                if (startTime != null && DateTime.Now - startTime.Value >= TimeSpan.FromMinutes(1))
                {
                    IEnumerable<Contentube> contentubes = _unitOfWork.Content.GetAll();
                    return View(contentubes);
                }
            }

            return View(contenttubeRepo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangeContentStatusfalse(int id)
        {
            // Retrieve the Contentube record from the database based on the provided id
            var contentube = _unitOfWork.Content.GetFirstOrDefault(u=>u.Id==id);

            if (contentube != null)
            {
                // Toggle the StatusContent property
                contentube.StatusContent = !contentube.StatusContent;

                // Mark the record as modified
                _unitOfWork.Content.Update(contentube);

                // Save the changes to the database
                _unitOfWork.Save();
            }
            return View();
        }
    }
}
