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

        public SidebarViewComponent(ISidebarService sidebarService)
        {
            _sidebarService = sidebarService;
        }

        public IViewComponentResult Invoke(int? materialId)
        {
            var comments = materialId.HasValue ? _sidebarService.GetComments(materialId.Value) : null;

            var favourites = _sidebarService.GetFavorites();
            var lastActivities = _sidebarService.GetLastActivities();

            SidebarViewModel vm = new SidebarViewModel
            {
                Favourites = favourites.Select(f =>
                    new FavouriteMaterialViewModel
                    {
                        Content = GetFavoriteMaterialBreadcrumbsPath(f.Material),
                        RouteValue = f.MaterialId
                    }
                ).ToList(),

                LastActivities = lastActivities.Select(a =>
                    new LastActivityViewModel
                    {
                        Content = a.Content,
                        When = FormatCreatedDate(a.Date),
                        RouteValue = a.Material.MaterialId,
                        Type = a.ActivityType,
                        Material = a.Material,
                    }
                ).ToList(),

                Comments = comments?.Select(c =>
                    new CommentViewModel
                    {
                        Content = c.Content,
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
