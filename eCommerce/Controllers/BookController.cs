using eCommerce.Data;
using eCommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Controllers;

public class BookController(BookShopDbContext context) : Controller
{
    private readonly BookShopDbContext _context = context;

    public async Task<IActionResult> Index(int page = 1, string? sortField = null, string? sortDir = null, string? searchTerm = null)
    {
        const int pageSize = 10; // Products per page

        sortField ??= "Title"; // default sort
        sortDir = string.Equals(sortDir, "desc", StringComparison.OrdinalIgnoreCase) ? "desc" : "asc";

        IQueryable<Book> query = _context.Books;

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            string term = searchTerm.ToUpper().Trim();
            query = query.Where(b => b.Title.ToUpper().Contains(term) || b.Author.ToUpper().Contains(term));
        }

        // SQLite cannot order directly by decimal (mapped as TEXT) reliably; cast to double for ordering.
        query = (sortField, sortDir) switch
        {
            ("Price", "asc") => query.OrderBy(b => (double)b.Price).ThenBy(b => b.Id),
            ("Price", "desc") => query.OrderByDescending(b => (double)b.Price).ThenBy(b => b.Id),
            ("Title", "desc") => query.OrderByDescending(b => b.Title).ThenBy(b => b.Id),
            _ => query.OrderBy(b => b.Title).ThenBy(b => b.Id)
        };

        int totalItems = await query.CountAsync();
        int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
        if (page < 1) page = 1;
        if (page > totalPages && totalPages > 0) page = totalPages;

        List<Book> books = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var vm = new BookListViewModel
        {
            Books = books,
            CurrentPage = page,
            TotalPages = totalPages,
            PageSize = pageSize,
            TotalItems = totalItems,
            SortField = sortField,
            SortDirection = sortDir,
            SearchTerm = searchTerm
        };

        return View(vm);
    }

    [HttpGet]
    public IActionResult Create()
    {
        // Require login
        if (HttpContext.Session.GetString("Username") == null)
        {
            TempData["ErrorMessage"] = "Please sign in to Create a book.";
            return RedirectToAction("Login", "Member");
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Book book)
    {
        // Require login
        if (HttpContext.Session.GetString("Username") == null)
        {
            TempData["ErrorMessage"] = "Please sign in to Create a book.";
            return RedirectToAction("Login", "Member");
        }

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
        // Require login
        if (HttpContext.Session.GetString("Username") == null)
        {
            TempData["ErrorMessage"] = "Please sign in to Edit a book.";
            return RedirectToAction("Login", "Member");
        }

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
        // Require login
        if (HttpContext.Session.GetString("Username") == null)
        {
            TempData["ErrorMessage"] = "Please sign in to Edit a book.";
            return RedirectToAction("Login", "Member");
        }

        if (ModelState.IsValid)
        {
            _context.Update(book);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Book \"{book.Title}\" updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        return View(book);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        // Require login
        if (HttpContext.Session.GetString("Username") == null)
        {
            TempData["ErrorMessage"] = "Please sign in to Delete a book.";
            return RedirectToAction("Login", "Member");
        }

        Book? book = await _context.Books.FindAsync(id);

        if (book == null)
        {
            return NotFound();
        }

        return View(book);
    }

    [ActionName(nameof(Delete))]
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        // Require login
        if (HttpContext.Session.GetString("Username") == null)
        {
            TempData["ErrorMessage"] = "Please sign in to Delete a book.";
            return RedirectToAction("Login", "Member");
        }

        Book? book = await _context.Books.FindAsync(id);

        if (book == null)
        {
            TempData["ErrorMessage"] = "Book not found or already deleted.";
            return RedirectToAction(nameof(Index));
        }

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = $"Book \"{book.Title}\" deleted successfully!";
        return RedirectToAction(nameof(Index));
    }
}
