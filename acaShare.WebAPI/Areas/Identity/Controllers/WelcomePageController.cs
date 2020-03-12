using Microsoft.AspNetCore.Mvc;

namespace acaShare.WebAPI.Areas.Identity.Controllers
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