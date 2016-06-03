using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;

namespace EnrollmentApplication.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public IActionResult Index() => View();

        public IActionResult AppLayout() => View();

        public IActionResult Welcome() => View();

    }
}
