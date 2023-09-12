using GrowUp.DataAccess.Repository;
using GrowUp.DataAccess.Repository.IRepository;
using GrowUp.Model;
using GrowUp.Model.ViewModels;
using GrowUp.Utility;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using NuGet.Protocol;
using System.Data;
using System.Diagnostics;
using System.Security.Claims;

namespace GrowUpSite.Areas.UserDashboard.Controllers
{
    [Area("UserDashboard")]
    [Authorize(Roles = StaticDetail.Role_User_Indi)]
    public class ContentController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public ContentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {

            // get the ID of the current user
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // retrieve all Contentube objects for the current user
            IEnumerable<Contentube> objContentList = _unitOfWork.Content.GetAll().Where(c => c.ApplicationUserId == userId);
            return View(objContentList);
        }

        public IActionResult Banned()
        {

            return View();
        }


        //GET
        public IActionResult Upsert(int? id)
        {
            //Content content = new Content();
            ContentVM contentVM = new()
            {
                Content = new(),

                ServiceListItem = _unitOfWork.Service.GetAll().Select(
                   u => new SelectListItem
                   {
                       Text = u.ServiceName,
                       Value = u.Id.ToString(),

                   }),


                CategoryListItem = _unitOfWork.Category.GetAll().Select(
                   u => new SelectListItem
                   {
                       Text = u.CategoryName,
                       Value = u.Id.ToString(),

                   }),

                CountryListItem = _unitOfWork.Country.GetAll().Select(
                   u => new SelectListItem
                   {
                       Text = u.CountryName,
                       Value = u.id.ToString(),

                   }),

            };

            if (id == null || id == 0)
            {
                //create
                return View(contentVM);
            }

            else
            {
                //update
                contentVM.Content = _unitOfWork.Content.GetFirstOrDefault(u => u.Id == id);
                return View(contentVM);
            }
        }




        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ContentVM obj)
        {

            if (ModelState.IsValid)
            {
                // Add User_id to the model state
                var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
                ModelState.SetModelValue("Content.User_id", new ValueProviderResult(userIdClaim));

                // Check if foreign key values exist in their respective tables
                var categoryExists = _unitOfWork.Category.GetFirstOrDefault(c => c.Id == obj.Content.Category_typeId) != null;
                var serviceExists = _unitOfWork.Service.GetFirstOrDefault(c => c.Id == obj.Content.Service_typeId) != null;
                var countryExists = _unitOfWork.Country.GetFirstOrDefault(c => c.id == obj.Content.Country_nameId) != null;

             
                if (!categoryExists)
                {
                    ModelState.AddModelError(nameof(ContentVM.Content.Category_typeId), "Invalid category type");
                    return View(obj);
                }
                if (!serviceExists)
                {
                    ModelState.AddModelError(nameof(ContentVM.Content.Service_typeId), "Invalid service type");
                    return View(obj);
                }
                if (!countryExists)
                {
                    ModelState.AddModelError(nameof(ContentVM.Content.Country_nameId), "Invalid country name");
                    return View(obj);
                }

                if (obj.Content.Id == 0)
                {
                    // Check if the Content_url already exists
                    var contentExists = _unitOfWork.Content.GetFirstOrDefault(c => c.Content_Url == obj.Content.Content_Url) != null;
                    if (contentExists)
                    {
                        TempData["error"] = "Content is already exist";
                        return View(obj);
                    }

                    _unitOfWork.Content.Add(obj.Content);
                    obj.Content.StatusContent = true;
                }
                else
                {
                    _unitOfWork.Content.Update(obj.Content);
                }

                _unitOfWork.Save();
                TempData["success"] = "Content is created successfully";
                return RedirectToAction("Index");
            }

            return View(obj);
        }


        //GET
        public IActionResult Show(int? id)
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

        private string GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }


        // count data from CurrentUser

        private int CountData()
        {
            string currentUserId = GetCurrentUserId();
            int countWatchtube = _unitOfWork.Watchtube.Count(w => w.ApplicationUserId == currentUserId);
            return countWatchtube;
        }

        // counting the current User's data that inserted by "others users"
        private int CountInsertedByOthers()
        {
            string currentUserId = GetCurrentUserId();

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
            int insertedByOthersCount = videoLinksInsertedByOthers.Count();

            return insertedByOthersCount;
        }

        public IActionResult Playlistube(string range)
        {
            string currentUserId = GetCurrentUserId();
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var User_id = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == userId);
            // retrieve all Contentube objects for the current user

            var checkContent = _unitOfWork.Content.GetFirstOrDefault(c=>c.ApplicationUserId == userId);
            int userReactubeCount = _unitOfWork.Reactube.Count(r => r.ApplicationUserId == currentUserId);
        

            if ( checkContent.StatusContent == true) { 

                if (userId != null)
                {
                    // Get the Category_typeId and Service_typeId of the current user's Contentube record
                    int categoryTypeId = _unitOfWork.Reactube.GetAll(r => r.ApplicationUserId == currentUserId, includeProperties: "Content").Select(r => r.Content.Category_typeId).FirstOrDefault();
                    int serviceTypeId = _unitOfWork.Reactube.GetAll(r => r.ApplicationUserId == currentUserId, includeProperties: "Content").Select(r => r.Content.Service_typeId).FirstOrDefault();


                    // Call the CountData method to get the count ofWatchtube records for the current user
                    int watchtubeCount = CountData();
                    int insertedByOthersCount = CountInsertedByOthers();

                    if (insertedByOthersCount > watchtubeCount)
                    {
                        ViewBag.InsertedByOthersCount = insertedByOthersCount;
                        ViewBag.WatchtubeCount = watchtubeCount;
                        ViewBag.StartTimer = true;
                    }
                    else
                    {
                        ViewBag.StartTimer = false;
                    }

                    var existingWatchtubeIds = _unitOfWork.Watchtube.GetAll().Select(w => w.ReactubeId);


//                    var reactubeList = _unitOfWork.Reactube.GetAll(
//    r => !existingWatchtubeIds.Contains(r.Id) &&
//        r.Content.Category_typeId == categoryTypeId &&
//        r.Content.Service_typeId == serviceTypeId &&
//        r.ItemVideo != currentUserId &&
//        r.ApplicationUserId != currentUserId &&
//        r.Status == true,
//    includeProperties: "Content"
//).OrderBy(r => r.Id);

                    // Get the Reactube records for the current content that have the same Category_typeId and Service_typeId,
                    // exclude the ones where ItemVideo is equal to currentUserId,
                    // exclude the ones where the Content's ApplicationUser is equal to currentUserId,
                    // and include only the ones where Status is false
                    IEnumerable<Reactube> reactubeList;
                    switch (range)
                    {
                        case "1-5":
                            reactubeList = _unitOfWork.Reactube.GetAll(
                                r => !existingWatchtubeIds.Contains(r.Id) &&
                                r.Content.Category_typeId == categoryTypeId &&
                                r.Content.Service_typeId == serviceTypeId &&
                                r.ItemVideo != currentUserId &&
                                r.ApplicationUserId != currentUserId &&
                                r.Status == true,
                            includeProperties: "Content"
                        ).OrderBy(r => r.Id).Take(5);
                            break;
                        case "1-10":
                            reactubeList = _unitOfWork.Reactube.GetAll(
                                r => !existingWatchtubeIds.Contains(r.Id) &&
                                r.Content.Category_typeId == categoryTypeId &&
                                r.Content.Service_typeId == serviceTypeId &&
                                r.ItemVideo != currentUserId &&
                                r.ApplicationUserId != currentUserId &&
                                r.Status == true,
                                includeProperties: "Content"
                            ).OrderBy(r => r.Id).Take(10);
                            break;
                        case "1-15":
                            reactubeList = _unitOfWork.Reactube.GetAll(
                                r => !existingWatchtubeIds.Contains(r.Id) &&
                                r.Content.Category_typeId == categoryTypeId &&
                                r.Content.Service_typeId == serviceTypeId &&
                                r.ItemVideo != currentUserId &&
                                r.ApplicationUserId != currentUserId &&
                                r.Status == true,
                                includeProperties: "Content"
                            ).OrderBy(r => r.Id).Take(15);
                            break;
                        case "1-20":
                            reactubeList = _unitOfWork.Reactube.GetAll(
                                r => !existingWatchtubeIds.Contains(r.Id) &&
                                r.Content.Category_typeId == categoryTypeId &&
                                r.Content.Service_typeId == serviceTypeId &&
                                r.ItemVideo != currentUserId &&
                                r.ApplicationUserId != currentUserId &&
                                r.Status == true,
                                includeProperties: "Content"
                            ).OrderBy(r => r.Id).Take(20);
                            break;


                        case "1-25":
                            reactubeList = _unitOfWork.Reactube.GetAll(
                                r => !existingWatchtubeIds.Contains(r.Id) &&
                                r.Content.Category_typeId == categoryTypeId &&
                                r.ItemVideo != currentUserId &&
                                r.ApplicationUserId != currentUserId &&
                                r.Status == true,
                                includeProperties: "Content"
                            ).OrderBy(r => r.Id).Take(25);
                            break;
                        default:
                            reactubeList = _unitOfWork.Reactube.GetAll(
                                r => !existingWatchtubeIds.Contains(r.Id) &&
                                r.Content.Category_typeId == categoryTypeId &&
                                r.Content.Service_typeId == serviceTypeId &&
                                r.ItemVideo != currentUserId &&
                                r.ApplicationUserId != currentUserId &&
                                r.Status == true,
                                includeProperties: "Content"
                            ).OrderBy(r => r.Id).Take(5);
                            break;
                    }

                    // Get the count of Reactube records for the current content where ItemVideo is equal to currentUserId
                    int itemCount = _unitOfWork.Reactube.Count(
                        r => !existingWatchtubeIds.Contains(r.Id) &&
                        r.Content.Category_typeId == categoryTypeId &&
                        r.Content.Service_typeId == serviceTypeId &&
                        r.ItemVideo == currentUserId &&
                        r.ApplicationUserId != currentUserId
                    );


                    // Pass the reactubeList, itemCount, and watchtubeCount to the view using a tuple
                    var model = (reactubeList, itemCount, watchtubeCount, insertedByOthersCount);
                    return View(model);
                }

            }
            else
            {
                return View("Banned");  
            }
            return RedirectToAction("Index");
        }




        [HttpPost]
        public IActionResult SaveVideos(int contentId, string[] videoIds)
        {
            var content = _unitOfWork.Content.GetFirstOrDefault(u => u.Id == contentId);
            if (content == null)
            {
                return NotFound();
            }

            if (videoIds != null && videoIds.Length > 0)
            {
                string currentUserId = GetCurrentUserId();

                foreach (var videoId in videoIds)
                {
                    var reactube = new Reactube
                    {
                        Content = content,
                        ItemVideo = videoId,
                        Status = true,
                        ApplicationUserId = currentUserId
                    };

                    _unitOfWork.Reactube.Add(reactube);
                }

                _unitOfWork.Save();

                return Ok();
            }

            // Return an error if no videos were selected
            return BadRequest("No videos were selected.");
        }

     
        //Reactube

        public IActionResult ReactubeList()
        {
            var userId = GetCurrentUserId();
            var contentlist = _unitOfWork.Content.GetFirstOrDefault(c => c.ApplicationUserId == userId);
            var reactlist = _unitOfWork.Reactube.GetFirstOrDefault(c => c.ApplicationUserId == userId);


            if (contentlist.StatusContent == true)
            {

                IEnumerable<Reactube> reactubeList = _unitOfWork.Reactube.GetAll().Where(c => c.ApplicationUserId == userId);

                if (reactubeList == null)
                {
                    return NotFound();// Message reccord is empty
                }
                return View(reactubeList);
            }

            else
            {
                var reactubesToUpdate = _unitOfWork.Reactube.GetAll(c => c.ContentId == contentlist.Id && c.ApplicationUserId == userId);

                if (reactubesToUpdate != null)
                {
                    foreach (var reactube in reactubesToUpdate)
                    {
                        reactube.Status = false;
                    }

                    _unitOfWork.Save();
                }

                return View("Banned");
            }

        }


        //ContentStatus  
        [HttpPost]
        public IActionResult UpdateStatusContent()
        {
            var currentTime = DateTime.Now;
            var startTime = TempData["StartTime"] as DateTime?; // Retrieve the start time from TempData

            if (startTime == null || currentTime.Subtract(startTime.Value).TotalMinutes >= 1)
            {
                // Retrieve the current user's contentube
                var currentUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var contentube = _unitOfWork.Content.GetFirstOrDefault(c => c.ApplicationUserId == currentUser);

                if (contentube != null && !string.IsNullOrEmpty(contentube.ApplicationUserId))
                {
                    contentube.StatusContent = false;
                    _unitOfWork.Save();
                }

                // Store the new start time in TempData
                TempData["StartTime"] = currentTime;
            }

            return RedirectToAction("Playlistube");
        }



        public ActionResult UpdateStatus(int? id)
        {
            var reactube = _unitOfWork.Reactube.GetFirstOrDefault(r => r.Id == id);

            if (reactube == null)
            {
                return NotFound();
            }

            reactube.Status = !reactube.Status; // toggle the status

            _unitOfWork.Reactube.Update(reactube);
            _unitOfWork.Save();


            return Json(new { id = reactube.Id, status = reactube.Status });

        }


        [HttpPost]
        public IActionResult AddToWatchtube(int reactubeId, string videoLink)
        {
            var currentUserId = GetCurrentUserId();
            var reactube = _unitOfWork.Reactube.GetFirstOrDefault(r => r.Id == reactubeId);
            if (reactube == null)
            {
                return Json(new { success = false, message = "Reactube not found" });
            }
            //reactube.Status = true;
            _unitOfWork.Reactube.Update(reactube);
            var watchtube = new Watchtube
            {
                ContentId = reactube.ContentId,
                ApplicationUserId = currentUserId,
                ReactubeId = reactubeId,
                Date = DateTime.Now,
                VideoLink = videoLink
            };
            _unitOfWork.Watchtube.Add(watchtube);
            _unitOfWork.Save();
            return Json(new { success = true });
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




       
    }


}

