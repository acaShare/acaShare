using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using acaShare.MVC.Models;
using acaShare.ServiceLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace acaShare.MVC.Areas.Moderator.Controllers
{
    [Authorize(Roles = Roles.AdministratorRole + ", " + Roles.MainModeratorRole + ", " + Roles.ModeratorRole)]
    [Area("Moderator")]
    public class StatisticsController : Controller
    {
        private readonly IStatisticsService _statisticsService;

        public StatisticsController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        public IActionResult Statistics()
        {
            ConfigureListBreadcrumbs();
            return View(_statisticsService.GetAvailableStatistics());
        }

        private void ConfigureListBreadcrumbs()
        {
            ViewBag.Breadcrumbs = new List<Breadcrumb>
            {
                new Breadcrumb
                {
                    Controller = "ModeratorPanel",
                    Action = "Statistics",
                    Title = "Statystyki"
                }
            };
        }

        public IActionResult DeleteRequestsStatistics()
        {
            ConfigureDeleteRequestsStatisticsBreadcrumbs();
            return View(_statisticsService.GetDeleteRequestsStatistics());
        }

        private void ConfigureDeleteRequestsStatisticsBreadcrumbs()
        {
            ViewBag.Breadcrumbs = new List<Breadcrumb>
            {
                new Breadcrumb
                {
                    Controller = "ModeratorPanel",
                    Action = "Statistics",
                    Title = "Statystyki"
                },
                new Breadcrumb
                {
                    Controller = "ModeratorPanel",
                    Action = "Statistics",
                    Title = "Statystyki sugestii usunięcia"
                }
            };
        }
    }
}