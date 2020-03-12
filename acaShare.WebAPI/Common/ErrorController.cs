using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace acaShare.WebAPI
{
    [Route("error")]
    public class ErrorController : Controller
    {
        // GET: /<controller>/
        [Route("404")]
        public IActionResult PageNotFound()
        {
            return View();
        }

        [Route("ResourceNotFound")]
        public IActionResult ResourceNotFound(string error)
        {
            return View("ResourceNotFound", error);
        }

        [Route("ActionForbidden")]
        public IActionResult ActionForbidden(string error)
        {
            return View("ActionForbidden", error);
        }
    }
}
