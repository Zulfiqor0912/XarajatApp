using Microsoft.AspNetCore.Mvc;
using XarajatApp.Services.Interfaces;

namespace XarajatApp.Controllers
{
    public class ExpenditureController(IExpenditureService expenditureService) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ExpenditureMenu()
        {
            return View();
        }

        public async Task<IActionResult> ShowAllGroupMembers(string groupname)
        {
            var membersViewModel = await expenditureService.GetAllGroupMembersByGroupname(groupname);
            return View(membersViewModel);
        }
    }
}
