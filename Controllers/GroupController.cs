using Microsoft.AspNetCore.Mvc;
using XarajatApp.Repositories.Interface;
using XarajatApp.ViewModels;

namespace XarajatApp.Controllers
{
    public class GroupController(
        IGroupRepasitory groupRepasitory,
        IUserRepository userRepository
        ) : Controller

    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Menu()
        {
            return View();
        }

        public async Task<IActionResult> ShowAllUsers()
        {
            var users = await userRepository.GetAllUsers();
            List<UserViewModel> usersViewModel = new List<UserViewModel>();
            if (users != null)
            {
                foreach (var item in users)
                {
                    usersViewModel.Add(new UserViewModel
                    {
                        Username = item.Username,
                        Fullname = item.Fullname,
                        CreatedDate = item.CreatedDate
                    });
                }
            }
            return View(usersViewModel); 
        }

        public async Task<IActionResult> CreateGroup()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateGroup(CreateGroupViewModel createGroupViewModel)
        {
            var result = await groupRepasitory.CreateGroup(createGroupViewModel);
            if (result.Succed)
            {
                return RedirectToAction("Menu");
            }
            else
            {
                TempData["Error"] = result.Message;
                return View(createGroupViewModel);
            }
        }

        public async Task<IActionResult> GetAllGroups()
        {
            var result = await groupRepasitory.ShowAllGroups();

            if (result.Succed)
            {
                return View(result.GroupsViewModel);
            }
            else
            {
                TempData["Error"] = result.Message;
                return View(new List<GetAllGroupsViewModel>());
            }
        }
    }
}
