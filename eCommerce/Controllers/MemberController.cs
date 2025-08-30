using eCommerce.Data;
using eCommerce.Models;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Controllers;

public class MemberController(BookShopDbContext context) : Controller
{
    private readonly BookShopDbContext _context = context;

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegistrationViewModel vm)
    {
        if (ModelState.IsValid)
        {
            // Map the ViewModel to the Member entity
            Member newMember = new()
            {
                Username = vm.Username,
                Password = vm.Password,
                Email = vm.Email,
                DateOfBirth = vm.DateOfBirth
            };
            _context.Members.Add(newMember);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }

        return View(vm);
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel login)
    {
        if (ModelState.IsValid)
        {
            Member? loggedInMember = await _context.Members
                                            .Where(m => (m.Username == login.UsernameOrEmail || m.Email == login.UsernameOrEmail)
                                                        && m.Password == login.Password)
                                            .SingleOrDefaultAsync();

            if (loggedInMember == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid Credentials");
                return View(login);
            }

            // Store member info in session
            HttpContext.Session.SetString("Username", loggedInMember.Username);
            HttpContext.Session.SetInt32("Id", loggedInMember.Id);

            return RedirectToAction("Index", "Home");
        }
        return View(login);
    }

    [HttpGet]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }
}
