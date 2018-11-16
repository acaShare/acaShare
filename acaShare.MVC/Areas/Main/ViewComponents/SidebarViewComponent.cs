﻿using acaShare.MVC.Areas.Main.Models.Sidebar;
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

        public IViewComponentResult Invoke()
        {
            SidebarViewModel vm = new SidebarViewModel
            {
                Favourites = new List<FavouriteMaterialViewModel>
                {
                    new FavouriteMaterialViewModel
                    {
                        Link = "",
                        When = DateTime.Today.ToShortDateString(),
                        Breadcrumbs = "aa -> bb"
                    },
                    new FavouriteMaterialViewModel
                    {
                        Link = "",
                        When = DateTime.Today.AddDays(-100).ToShortDateString(),
                        Breadcrumbs = "ff -> ss"
                    },
                    new FavouriteMaterialViewModel
                    {
                        Link = "",
                        When = DateTime.Today.AddDays(-10).ToShortDateString(),
                        Breadcrumbs = "tt -> qq"
                    }
                },
                LastActivities = new List<LastActivityViewModel>
                {
                    new LastActivityViewModel
                    {
                        Name = "Użytkownik Maciej Sadoś dodał komentarz do materiału \"Jak zaliczyć PJATK\"",
                        Link = "",
                        Type = LastActivityType.COMMENT,
                        When = DateTime.Today.AddDays(-10).ToShortDateString()
                    },
                    new LastActivityViewModel
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
