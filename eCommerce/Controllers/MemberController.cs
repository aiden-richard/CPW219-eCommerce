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
}
