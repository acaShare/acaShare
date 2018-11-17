using acaShare.BLL.Models;
using acaShare.MVC.Areas.Main.Models.Sidebar;
using acaShare.MVC.Models;
using acaShare.ServiceLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace acaShare.MVC.Areas.Main.ViewComponents
{
    public class SidebarViewComponent : ViewComponent
    {
        private readonly ISidebarService _sidebarService;
        private readonly IUserService _userService;

        public SidebarViewComponent(ISidebarService sidebarService, IUserService userService)
        {
            _sidebarService = sidebarService;
            _userService = userService;
        }

        public IViewComponentResult Invoke(int? materialId, string loggedUserId)
        {
            var comments = materialId.HasValue ? _sidebarService.GetComments(materialId.Value) : null;

            var loggedUser = _userService.FindByIdentityUserId(loggedUserId);
            var favoriteMaterials = loggedUser.GetFavoriteMaterials();

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
                        //Content = a.Content,
                        When = FormatCreatedDate(a.Date),
                        RouteValue = a.Material.MaterialId,
                        Type = a.ActivityType,
                        Material = a.Material,
                    }
                ).ToList(),

                Comments = comments?.Select(c =>
                    new CommentViewModel
                    {
                        //Content = c.Content,
                        When = FormatCreatedDate(c.CreatedDate),
                        CommentId = c.CommentId,
                        Author = c.User.Username
                    }
                ).ToList(),

                MaterialId = materialId ?? -1
            };

            return View(vm);
        }

        private string FormatCreatedDate(DateTime createdDate)
        {
            return createdDate.ToShortDateString();
        }

        private string GetFavoriteMaterialBreadcrumbsPath(Material material)
        {
            var lesson = material.Lesson;
            var subjectDepartment = lesson.SubjectDepartment;
            var department = subjectDepartment.Department;
            var university = department.University;
            var semester = lesson.Semester;
            
            return $"{university.Abbreviation} -> {department.Abbreviation} -> {semester.Number} -> " +
                   $"{ subjectDepartment.Subject.Abbreviation} -> {material.Name}";
        }
    }
}
