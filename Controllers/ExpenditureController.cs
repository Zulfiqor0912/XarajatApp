using Microsoft.AspNetCore.Mvc;
using XarajatApp.Services.Interfaces;
using XarajatApp.ViewModels;

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
            var model = new ExpenditureMenuViewModel
            {
                Groupname = HttpContext.Session.GetString("Groupname") ?? "0"
            };
            return View(model);
        }

        public async Task<IActionResult> ShowAllGroupMembers(string groupname)
        {
            var result = await expenditureService.GetAllGroupMembersByGroupname(groupname);
            if (result.Succed)
                return View(result.GroupMembersViewModel);
            else
            {
                TempData["Error"] = result.Message;
                return View();
            }
        }

        public async Task<IActionResult> CreateExpenditure()
        {
            return View();
        }
    }
}
