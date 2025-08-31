using System.Collections.Generic;

namespace eCommerce.Models;

public class BookListViewModel
{
    public required IEnumerable<Book> Books { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
    public string? SortField { get; set; }
    public string? SortDirection { get; set; } // "asc" or "desc"
    public string? SearchTerm { get; set; } // current search query
}
