using imtahan.me.Models;
using imtahan.me.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace imtahan.me.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager
        {
            get;
        }
        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInVM signIn, string ReturnUrl)
        {
            AppUser user;
            if (signIn.UsernameOrEmail.Contains("@"))
            {
                user = await _userManager.FindByEmailAsync(signIn.UsernameOrEmail);
            }
            else
            {
                user = await _userManager.FindByNameAsync(signIn.UsernameOrEmail);
            }
            if (user == null)
            {
                ModelState.AddModelError("", "Login ve ya parol yalnisdir");
                return View(signIn);
            }
            var result = await
            _signInManager.PasswordSignInAsync(user, signIn.Password, signIn.RememberMe, true);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Login ve ya parol yalnisdir");
                return View(signIn);
            }
            if (ReturnUrl != null) return LocalRedirect(ReturnUrl);
            return RedirectToAction("Index", "Team", new
            {
                area = "Admin"
            });
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            if (!ModelState.IsValid) return View();
            AppUser newUser = new AppUser
            {
                Email = register.Email,
                UserName = register.Username
            };
            IdentityResult result = await _userManager.CreateAsync(newUser, register.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return RedirectToAction("SignIn");
        }
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(SignIn));
        }
    }
}
