using Microsoft.AspNetCore.Mvc;

namespace XarajatApp.Controllers
{
    public class ExpenditureController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ExpenditureMenu()
        {
            return View();
        }
    }
}
