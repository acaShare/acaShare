using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using acaShare.MVC.Areas.Universities.Models;
using Microsoft.AspNetCore.Authorization;
using acaShare.MVC.Areas.Universities.Models.Sidebar;

namespace acaShare.MVC.Areas.Universities.Controllers
{
    [Authorize]
    [Area("Universities")]
    public class ListController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            SidebarViewModel vm = new SidebarViewModel
            {
                Favourites = new List<FavouriteMaterialsVM>
                {
                    new FavouriteMaterialsVM
                    {
                        Link = "",
                        When = DateTime.Today.ToShortDateString(),
                        Breadcrumbs = "aa -> bb"
                    },
                    new FavouriteMaterialsVM
                    {
                        Link = "",
                        When = DateTime.Today.AddDays(-100).ToShortDateString(),
                        Breadcrumbs = "ff -> ss"
                    },
                    new FavouriteMaterialsVM
                    {
                        Link = "",
                        When = DateTime.Today.AddDays(-10).ToShortDateString(),
                        Breadcrumbs = "tt -> qq"
                    },
                },
                LastActivities = new List<LastActivityVM>
                {
                    new LastActivityVM
                    {
                        Name = "Użytkownik Maciej Sadoś dodał komentarz do materiału \"Jak zaliczyć PJATK\"",
                        Link = "",
                        Type = LastActivityType.COMMENT,
                        When = DateTime.Today.AddDays(-10).ToShortDateString()
                    },
                    new LastActivityVM
                    {
                        Name = "Użytkownik Michał Skotnicki dodał materiał \"GRK - kompedium wiedzy\"",
                        Link = "",
                        Type = LastActivityType.MATERIAL_ADD,
                        When = DateTime.Today.AddDays(-10).ToShortDateString()
                    }
                }
            };

            return View(vm);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
