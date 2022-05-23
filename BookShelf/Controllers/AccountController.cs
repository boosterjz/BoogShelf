using BookShelf.Models;
using BookShelf.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookShelf.Controllers;

public class AccountController : Controller {
    private readonly UserManager<BookShelfUser> _userManager;
    private readonly SignInManager<BookShelfUser> _signInManager;
    private readonly StoreDbContext _context;

    public AccountController(UserManager<BookShelfUser> userMgr,
        SignInManager<BookShelfUser> signInMgr,
        StoreDbContext context) {
        _userManager = userMgr;
        _signInManager = signInMgr;
        _context = context;
    }

    public ViewResult Login(string returnUrl) {
        return View(new LoginModel {
            ReturnUrl = returnUrl
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginModel loginModel) {
        if (!ModelState.IsValid) return View(loginModel);
        
        var user = await _userManager.FindByNameAsync(loginModel.Name);
        if (user != null) { 
            await _signInManager.SignOutAsync();
            if ((await _signInManager.PasswordSignInAsync(user,
                    loginModel.Password, loginModel.Remember, false)).Succeeded) {
                return Redirect(loginModel.ReturnUrl);
            }
        }
        
        ModelState.AddModelError("", "Invalid name or password");
        return View(loginModel);
    }
    
    public ViewResult Register(string returnUrl) {
        return View(new RegisterModel {
            ReturnUrl = returnUrl
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterModel registerModel)
    {
        if (!ModelState.IsValid) return View(registerModel);
        
        if (registerModel.Name == null)
        {
            ModelState.AddModelError("", "Name could not be empty");
            return View(registerModel);
        }

        if (registerModel.Password != registerModel.PasswordConfirm)
        {
            ModelState.AddModelError("", "Password and password confirmation should be equal");
            return View(registerModel);
        }

        var user = await _userManager.FindByNameAsync(registerModel.Name);
        if (user == null)
        {
            var newUser = new BookShelfUser(registerModel.Name);
            var result = await _userManager.CreateAsync(newUser, registerModel.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(newUser, registerModel.Remember);
                return Redirect(registerModel.ReturnUrl);
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(registerModel);
        }
        
        ModelState.AddModelError("", "User already exists");
        return View(registerModel);
    }

    [Authorize]
    public async Task<ViewResult> UserPage() {
        var user = await _userManager.FindByNameAsync(User.Identity?.Name);
        var orders = _context.Orders.Where(o => o.UserId == user.Id);
        return View(orders);
    }

    [Authorize(Roles = "admin")]
    public RedirectToPageResult AdminPage()
    {
        return RedirectToPage("/Admin/Index");
    }

    [Authorize]
    public async Task<RedirectResult> Logout(string returnUrl = "/") {
        await _signInManager.SignOutAsync();
        return Redirect(returnUrl);
    }
}