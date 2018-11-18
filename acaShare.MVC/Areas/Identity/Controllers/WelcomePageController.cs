using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace acaShare.MVC.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class WelcomePageController : Controller
    {
        public IActionResult WelcomePage()
        {
            return View("Areas/Identity/Pages/Account/WelcomePage.cshtml");
        }
    }
}