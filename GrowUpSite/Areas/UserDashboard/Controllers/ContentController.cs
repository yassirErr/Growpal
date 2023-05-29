﻿using GrowUp.DataAccess.Repository.IRepository;
using GrowUp.Model;
using GrowUp.Model.ViewModels;
using GrowUp.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Hosting;
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
            IEnumerable<Contentube> objContentList = _unitOfWork.Content.GetAll().Where(c => c.Country_nameId == userId);

            return View(objContentList);

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

                ApplicationUserListItem = _unitOfWork.ApplicationUser.GetAll().Select(
                   u => new SelectListItem
                   {
                       Text = u.County,
                       Value = u.Id.ToString(),

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


        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ContentVM obj)
        {

            if (ModelState.IsValid)
            {

                if (obj.Content.Id == 0)
                {
                    _unitOfWork.Content.Add(obj.Content);
                }
                else
                {
                    _unitOfWork.Content.Update(obj.Content);
                }
                _unitOfWork.Save();
                TempData["success"] = "Content is ceated successfully";
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
                foreach (var videoId in videoIds)
                {
                    var reactube = new Reactube
                    {
                        Content = content,
                        ItemVideo = videoId,
                        Status = true
                    };

                    _unitOfWork.Reactube.Add(reactube);
                }

                _unitOfWork.Save();

                return Ok();
            }

            // Return an error if no videos were selected
            return BadRequest("No videos were selected.");
        }



        private string GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public IActionResult Playlistube()
        {
            
            string currentUserId = GetCurrentUserId();
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            int userReactubeCount = _unitOfWork.Reactube.Count(
            r => r.Content.Country_nameId == currentUserId);

            if (userId != null)
            {
                // Get the Category_typeId and Service_typeId of the current user's Contentube record
                int categoryTypeId = _unitOfWork.Content.GetFirstOrDefault(c => c.Country_nameId == currentUserId)?.Category_typeId ?? 0;
                int serviceTypeId = _unitOfWork.Content.GetFirstOrDefault(c => c.Country_nameId == currentUserId)?.Service_typeId ?? 0;

                // Get the Reactube records for the current content that have the same Category_typeId and Service_typeId,
                // exclude the ones where ItemVideo is equal to currentUserId,
                // and exclude the ones where the Content's ApplicationUser is equal to currentUserId
                IEnumerable<Reactube> reactubeList = _unitOfWork.Reactube.GetAll(
                   r => r.Content.Category_typeId == categoryTypeId &&
                   r.Content.Service_typeId == serviceTypeId &&
                   r.ItemVideo != currentUserId &&
                   r.Content.Country_nameId != currentUserId,
                   includeProperties: "Content"
               ).OrderBy(r => r.Id).Take(userReactubeCount);

                // Get the count of Reactube records for the current content where ItemVideo is equal to currentUserId
                int itemCount = _unitOfWork.Reactube.Count(
                    r => r.Content.Category_typeId == categoryTypeId &&
                    r.Content.Service_typeId == serviceTypeId &&
                    r.ItemVideo == currentUserId &&
                    r.Content.Country_nameId != currentUserId
                );

                // Pass the reactubeList and itemCount to the view using a tuple
                var model = (reactubeList, itemCount);
                return View(model);
            }
            return RedirectToAction("Index");
        }


        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            //var obj = _db.Categories.Find(id);
            var contentFormDbFirst = _unitOfWork.Content.GetFirstOrDefault(u => u.Id == id);
            if (contentFormDbFirst == null)
            {
                return NotFound();
            }

            _unitOfWork.Content.Remove(contentFormDbFirst);
            _unitOfWork.Save();
            TempData["success"] = "Content Deleted successfully";
            return RedirectToAction("Index");


        }
    }
}
