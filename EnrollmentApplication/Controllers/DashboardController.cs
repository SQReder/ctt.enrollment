using Microsoft.AspNet.Mvc;

namespace EnrollmentApplication.Controllers
{
    public class DashboardController: Controller
    {
        public IActionResult Layout() => View();
    }
}
