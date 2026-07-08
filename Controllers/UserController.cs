using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using XarajatApp.Models;
using XarajatApp.Repositories;
using XarajatApp.ViewModel;

namespace XarajatApp.Controllers;

public class UserController(UserRepository userRepository) : Controller
{
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult Registration()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Registration(RegisterViewModel registerViewModel)
    {
        userRepository.Register(registerViewModel);
        return RedirectToAction("Index");
    }
}
