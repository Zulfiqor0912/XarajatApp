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

        [HttpPost]
        public async Task<IActionResult> CreateExpenditure(CreateExpenditureViewModel expenditureViewModel)
        {
            expenditureViewModel.TeamName = HttpContext.Session.GetString("Groupname") ?? "0";
            var result = await expenditureService.CreateExpenditure(expenditureViewModel);
            if (result.Succed)
            {
                return RedirectToAction("ExpenditureMenu");
            }
            else
            {
                TempData["Error"] = result.Succed;
                return View(expenditureViewModel);
            }
        }

        public async Task<IActionResult> ExpenditureHistory()
        {
            var groupname = HttpContext.Session.GetString("Groupname") ?? "0";
            var result = await expenditureService.GetAllExpenditures(groupname);
            if (result.Succed)
            {
                return View(result.GetAllExpenditures);
            }
            else
            {
                TempData["Error"] = result.Message;
                return View();
            }
        }

        public async Task<IActionResult> Calculate()
        {

            var result = await expenditureService.GetAllCalculateExpenditures(HttpContext.Session.GetString("Groupname") ?? "0");
            if (result.Succed)
            {
                return View(result.AllCalculateExpenditures);
            }
            else
            {
                TempData["Error"] = result.Message;
                return View(TempData);
            }

            
        }
    }
}
