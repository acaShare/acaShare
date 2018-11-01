using acaShare.MVC.Areas.Universities.Models.Sidebar;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace acaShare.MVC.Areas.Universities.ViewComponents
{
    public class SidebarViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
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
    }
}
