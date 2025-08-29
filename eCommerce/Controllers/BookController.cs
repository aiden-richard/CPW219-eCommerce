using eCommerce.Data;
using eCommerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Controllers;

public class BookController(BookDbContext context) : Controller
{
    private readonly BookDbContext _context = context;

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Book book)
    {
        if (ModelState.IsValid)
        {
            // Add to database
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            // Redirect to a confirmation or list page
            return RedirectToAction(nameof(Index));
        }

        return View(book);
    }
}
