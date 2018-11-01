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
    public class StructureManagementController : Controller
    {
        public IActionResult Home()
        {
            return View();
        }
    }
}