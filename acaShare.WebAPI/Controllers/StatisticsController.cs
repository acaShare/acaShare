using acaShare.ServiceLayer.Interfaces;
using acaShare.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace acaShare.WebAPI.Controllers
{
    [Authorize(Roles = Roles.AdministratorRole + ", " + Roles.MainModeratorRole + ", " + Roles.ModeratorRole)]
    [Area("Moderator")]
    public class StatisticsController : Controller//Base
    {
        private readonly IStatisticsService _statisticsService;

        public StatisticsController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        public IActionResult Statistics()
        {
            ConfigureListBreadcrumbs();
            return View("StatisticsList", _statisticsService.GetAvailableStatistics());
        }

        private void ConfigureListBreadcrumbs()
        {
            ViewBag.Breadcrumbs = new List<Breadcrumb>
            {
                new Breadcrumb
                {
                    Controller = "Statistics",
                    Action = "Statistics",
                    Title = "Statystyki"
                }
            };
        }

        public IActionResult DeleteRequestsStatistics()
        {
            ConfigureDeleteRequestsStatisticsBreadcrumbs();
            return View("StatisticsList", _statisticsService.GetAvailableDeleteRequestsStatistics());
        }

        private void ConfigureDeleteRequestsStatisticsBreadcrumbs()
        {
            ViewBag.Breadcrumbs = new List<Breadcrumb>
            {
                new Breadcrumb
                {
                    Controller = "Statistics",
                    Action = "Statistics",
                    Title = "Statystyki"
                },
                new Breadcrumb
                {
                    Controller = "Statistics",
                    Action = "DeleteRequestsStatistics",
                    Title = "Statystyki sugestii usunięcia"
                }
            };
        }

        public IActionResult DeleteRequestsGroupedByReason()
        {
            ConfigureGetDeleteRequestsGroupedByReasonBreadcrumbs();
            return View(_statisticsService.GetDeleteRequestsGroupedByReason());
        }

        private void ConfigureGetDeleteRequestsGroupedByReasonBreadcrumbs()
        {
            ViewBag.Breadcrumbs = new List<Breadcrumb>
            {
                new Breadcrumb
                {
                    Controller = "Statistics",
                    Action = "Statistics",
                    Title = "Statystyki"
                },
                new Breadcrumb
                {
                    Controller = "Statistics",
                    Action = "DeleteRequestsStatistics",
                    Title = "Statystyki sugestii usunięcia"
                },
                new Breadcrumb
                {
                    Controller = "Statistics",
                    Action = "DeleteRequestsGroupedByReason",
                    Title = "Sugestie usunięcia pogrupowane według przyczyny"
                }
            };
        }
    }
}