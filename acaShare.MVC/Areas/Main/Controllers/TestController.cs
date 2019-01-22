using acaShare.BLL.Models;
using acaShare.MVC.Areas.Main.Models.Sidebar;
using acaShare.MVC.Models;
using acaShare.ServiceLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace acaShare.MVC.Areas.Main.Controllers
{
    [Area("Main")]
    public class TestController : Controller
    {
        private readonly ISidebarService _sidebarService;

        public TestController(ISidebarService sidebarService)
        {
            _sidebarService = sidebarService;
        }

        public IActionResult Invoke(int? materialId, string loggedUserId)
        {
            var comments = materialId.HasValue ? _sidebarService.GetComments(materialId.Value) : null;
            var favoriteMaterials = _sidebarService.GetFavoriteMaterials(loggedUserId);
            var lastActivities = _sidebarService.GetLastActivities();

            SidebarViewModel vm = new SidebarViewModel
            {
                Favourites = favoriteMaterials.Select(f =>
                    new FavouriteMaterialViewModel
                    {
                        Content = GetFavoriteMaterialBreadcrumbsPath(f),
                        RouteValue = f.MaterialId
                    }
                ).ToList(),

                LastActivities = lastActivities.Select(a =>
                    new LastActivityViewModel
                    {
                        When = FormatCreatedDate(a.Date),
                        RouteValue = a.Material.MaterialId,
                        Type = a.ActivityType,
                        Material = a.Material,
                        Who = a.Username
                    }
                ).ToList(),

                Comments = comments?.OrderByDescending(c => c.CreateDate).Select(c =>
                    new CommentViewModel
                    {
                        Content = c.Content,
                        When = FormatCreatedDate(c.CreateDate),
                        CommentId = c.CommentId,
                        Author = c.User.Username
                    }
                ).ToList(),

                MaterialId = materialId ?? -1
            };

            return PartialView(vm);
        }

        private string FormatCreatedDate(DateTime createdDate)
        {
            var now = DateTime.Now;
            var diffBetweenNowAndCreatedDate = (now - createdDate);

            var daysBetweenNowAndCreateDate = diffBetweenNowAndCreatedDate.Days;

            string elapsedTime = string.Empty;

            if (daysBetweenNowAndCreateDate == 0)
            {
                var hoursBetweenNowAndCreateDate = diffBetweenNowAndCreatedDate.Hours;
                if (hoursBetweenNowAndCreateDate == 0)
                {
                    var minutesBetweenNowAndCreateDate = diffBetweenNowAndCreatedDate.Minutes;
                    elapsedTime = $"{minutesBetweenNowAndCreateDate.ToString()} min.";
                }
                else
                {
                    elapsedTime = $"{hoursBetweenNowAndCreateDate.ToString()} godz.";
                }
            }
            else
            {
                elapsedTime = createdDate.ToString("d.MM.yyyy, HH:mm");
            }

            return elapsedTime;
        }

        private string GetFavoriteMaterialBreadcrumbsPath(Material material)
        {
            var lesson = material.Lesson;
            var subject = lesson.Subject;
            var department = lesson.Department;
            var university = department.University;
            var semester = lesson.Semester;
            
            return $"{university.Abbreviation} -> {department.Abbreviation} -> {semester.Number} -> " +
                   $"{subject.Abbreviation} -> {material.Name}";
        }
    }
}
