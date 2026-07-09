using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using XarajatApp.Models;
using XarajatApp.Repositories;
using XarajatApp.Repositories.Interface;
using XarajatApp.ViewModels;

namespace XarajatApp.Controllers;

public class UserController(
    IUserRepository userRepository,
    IGroupRepasitory groupRepasitory) : Controller
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
    public async Task<IActionResult> Registration(RegisterViewModel registerViewModel)
    {
        var b = await userRepository.Register(registerViewModel);
        if (b)
        {
            return RedirectToAction("Login");
        }
        else
        {
            TempData["Error"] = "Bu foydalanuvchi allaqachon ro'yxatdan o'tgan.";
            return View(registerViewModel);
        }
    }


    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
        //return RedirectToAction("Group");
        var result = await userRepository.Login(loginViewModel);
        if (result.Succed)
        {
            HttpContext.Session.SetString("Username", loginViewModel.Username);
            return RedirectToAction("Menu", "Group");
        }
        else
        {
            TempData["Error"] = result.Message;
            return View(loginViewModel);
        }
    }
}
