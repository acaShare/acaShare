using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace acaShare.MVC.Areas.Moderator.Controllers.MaterialChangeRequests
{
    [Area("Moderator")]
    public class DeleteSuggestionsController : Controller
    {
        public IActionResult DeleteSuggestions()
        {
            return View();
        }
    }
}