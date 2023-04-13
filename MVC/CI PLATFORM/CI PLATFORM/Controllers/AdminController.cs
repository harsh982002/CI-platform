using Microsoft.AspNetCore.Mvc;

namespace CI_PLATFORM.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("/Admin/CMS")]
        public IActionResult CMS()
        {
            return View();
        }

        [Route("/Admin/User_CMS")]
        public IActionResult User_CMS()
        {
            return View();
        }

        [Route("/Admin/Theme")]
        public IActionResult Mission_Theme_CMS()
        {
            return View();
        }
    }
}
