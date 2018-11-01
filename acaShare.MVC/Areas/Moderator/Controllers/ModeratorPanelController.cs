using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace acaShare.MVC.Areas.Moderator.Controllers
{
    [Authorize]
    [Area("Moderator")]
    public class ModeratorPanelController : Controller
    {
        public IActionResult Home()
        {
            ViewBag.IsModeratorPanel = true;
            return View();
        }
    }
}