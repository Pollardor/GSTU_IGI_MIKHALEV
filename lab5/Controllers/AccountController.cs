using lab2.Models.AccountModel;
using lab2.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab2.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IServiceProvider _serviceProvider;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, System.IServiceProvider serviceProvider)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _serviceProvider = serviceProvider;
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string fromEdit)
        {
            if (ModelState.IsValid)
            {
                User user = new User { Email = model.Email, UserName = model.Login };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                    if (fromEdit == null)
                    {
                        await _signInManager.SignInAsync(user, false);
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            else
            {
                return View(model);
            }
            return View(model);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.Login, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {

                    return RedirectToAction("Index", "Home");
                }

            }
            else
            {
                return View(model);
            }
            return View(model);

        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> UserEdit(string userName)
        {
            UserManager<User> userManager = _serviceProvider.GetRequiredService<UserManager<User>>();
            RoleManager<IdentityRole> roleManager = _serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            ViewBag.Roles = roleManager.Roles.ToList();
            return View("EditSelected", _userManager.Users.Where(x => x.UserName == userName).FirstOrDefault());
        }
        [Authorize(Roles = "admin")]
        public ActionResult EditingUsers()
        {
            return View(_userManager.Users.ToList());
        }
        [Authorize(Roles = "admin")]
        public ActionResult AddNewUser()
        {
            return View();
        }
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> SaveUser(User user, string role, string userName)
        {

            var userS = _userManager.Users.Where(x => x.UserName == userName).FirstOrDefault();
            await _userManager.SetUserNameAsync(userS, user.UserName);
            await _userManager.AddToRoleAsync(userS, role);
            var roles = await _userManager.GetRolesAsync(userS);
            await _userManager.RemoveFromRolesAsync(userS, roles.ToArray());
            await _userManager.AddToRoleAsync(userS, role);
            return View("EditingUsers", _userManager.Users.ToList());
        }
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> UserDeleting(string userName)
        {
            await _userManager.DeleteAsync(_userManager.Users.Where(x => x.UserName == userName).FirstOrDefault());
            return Redirect("EditingUsers");
        }

    }
}
