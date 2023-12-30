
using FerozLeisureHub.Application;
using FerozLeisureHub.Application.Common.Utility;
using FerozLeisureHub.Application.Services.Interfaces;
using FerozLeisureHub.Domain.Entities;
using FerozLeisureHub.Web.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace FerozLeisureHub.Web.Controllers
{

    public class AccountController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager,IRoleService roleService) : Controller
    {

        public IActionResult Login(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            var loginVM = new LoginVM
            {
                RedirectUrl = returnUrl
            };
            return View(loginVM);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");

        }

        public IActionResult AccessDenied()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(loginVM.Email, loginVM.Password, loginVM.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {

                    if (string.IsNullOrEmpty(loginVM.RedirectUrl))
                    {
                        return RedirectToAction("Index", "Home");

                    }
                    else
                    {
                        return LocalRedirect(loginVM.RedirectUrl);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Login attempt.");
                }
            }
            return View(loginVM);
        }


        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Register(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            if (!roleManager.RoleExistsAsync(SD.Role_Admin).GetAwaiter().GetResult())
            {
                roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).Wait();
                roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).Wait();
            }
            var registerVM = new RegisterVM
            {
                RoleList=roleService.GetRolesAsSelectListItems(),
                RedirectUrl = returnUrl
            };
            return View(registerVM);
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new()
                {
                    Name = registerVM.Name,
                    Email = registerVM.Email,
                    PhoneNumber = registerVM.PhoneNumber,
                    NormalizedEmail = registerVM.Email.ToUpper(),
                    EmailConfirmed = true,
                    UserName = registerVM.Email,
                    CreatedAt = DateTime.Now,
                };

                var result = await userManager.CreateAsync(user, registerVM.Password);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(registerVM.Role))
                    {
                        await userManager.AddToRoleAsync(user, registerVM.Role);
                    }
                    else
                    {
                        await userManager.AddToRoleAsync(user, SD.Role_Customer);
                    }
                    await signInManager.SignInAsync(user, isPersistent: false);
                    if (string.IsNullOrEmpty(registerVM.RedirectUrl))
                    {
                        return RedirectToAction("Index", "Home");

                    }
                    else
                    {
                        return LocalRedirect(registerVM.RedirectUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }


            registerVM.RoleList=roleService.GetRolesAsSelectListItems();
               
            return View(registerVM);
        }

    }
}