using eCommerce.Data;
using eCommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Controllers;

public class BookController(BookDbContext context) : Controller
{
    private readonly BookDbContext _context = context;

    public async Task<IActionResult> Index()
    {
        List<Book> allBooks = await _context.Books.ToListAsync();
        return View(allBooks);
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

            TempData["SuccessMessage"] = $"Book \"{book.Title}\" created successfully!";

            // Redirect to a confirmation or list page
            return RedirectToAction(nameof(Index));
        }

        return View(book);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        Book? book = await _context.Books.FindAsync(id);

        if (book == null)
        {
            return NotFound();
        }

        return View(book);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Book book)
    {
        if (ModelState.IsValid)
        {
            _context.Update(book);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Book \"{book.Title}\" updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        return View(book);
    }
}
