using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using acaShare.MVC.Areas.Moderator.Models.StructureManagement;
using acaShare.ServiceLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace acaShare.MVC.Areas.Moderator.Controllers
{
    [Authorize]
    [Area("Moderator")]
    public class ModeratorPanelController : Controller
    {
        private readonly IUniversityTreeTraversalService _service;

        public ModeratorPanelController(IUniversityTreeTraversalService service)
        {
            _service = service;
        }

        public IActionResult Home()
        {
            ViewBag.IsRoot = true;
            return View();
        }
    }
}