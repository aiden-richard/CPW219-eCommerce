using System.ComponentModel.DataAnnotations;

namespace eCommerce.Models;

/// <summary>
/// This class represents a Book entity in the eCommerce application.
/// It stores the following required information about a book: Id, Title, Author, Published date, Price, and ISBN.
/// It also includes an optional description field.
/// </summary>
public class Book
{
    /// <summary>
    /// The primary key for the Book in the database.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// The title of the book.
    /// </summary>
    [Required]
    [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
    public string? Title { get; set; }

    /// <summary>
    /// The author of the book.
    /// </summary>
    [Required]
    [StringLength(100, ErrorMessage = "Author name cannot exceed 100 characters.")]
    public string? Author { get; set; }

    /// <summary>
    /// Publication date of the book.
    /// </summary>
    [DataType(DataType.Date)]
    [Display(Name = "Publication Date")]
    public DateTime PublishedDate { get; set; }

    /// <summary>
    /// The price of the book, stored as a decimal.
    /// </summary>
    [Range(0, 10_000, ErrorMessage = "Price cannot be more than $10,000.")]
    [DataType(DataType.Currency)]
    public decimal Price { get; set; }

    /// <summary>
    /// The ISBN number of the book.
    /// </summary>
    [Required]
    [StringLength(13, MinimumLength = 10, ErrorMessage = "ISBN cannot exceed 13 characters.")]
    public string? ISBN { get; set; }

    /// <summary>
    /// The description or summary of the book. Optional field.
    /// </summary>
    [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
    public string? Description { get; set; }
}
