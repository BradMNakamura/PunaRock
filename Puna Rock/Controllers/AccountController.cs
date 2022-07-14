using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Puna_Rock.Services;
using Puna_Rock.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using System.Dynamic;
using SheetsQuickstart;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;

namespace Puna_Rock.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<AppUser> UserMgr { get; }
        private SignInManager<AppUser> SignInMgr { get; }
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            UserMgr = userManager;
            SignInMgr = signInManager;
        }
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login userModel)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await UserMgr.FindByEmailAsync(userModel.Email);
                if (user != null)
                {
                    await SignInMgr.SignOutAsync();
                    var result = await SignInMgr.PasswordSignInAsync(user, userModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(HomeController.Index), "Home");
                    }
                }
                ModelState.AddModelError(nameof(userModel.Email), "Login Failed: Invalid Email or Password");
            }
            return View(userModel);
        }

        public async Task<IActionResult> Logout()
        {
            await SignInMgr.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Register userModel)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser()
                {
                    UserName = userModel.Email,
                    Email = userModel.Email,
                };
                var result = await UserMgr.CreateAsync(user, userModel.Password);
                if (result.Succeeded)
                {
                    await SignInMgr.SignInAsync(user,false);
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
            }
            return View(userModel);
        }

    }
}
