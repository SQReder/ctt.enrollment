using Microsoft.AspNetCore.Mvc;

namespace Enrollment.Web.Controllers
{
    public class DashboardController: Controller
    {
        public IActionResult Layout() => View();
    }
}
