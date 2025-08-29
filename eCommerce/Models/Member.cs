using System.ComponentModel.DataAnnotations;

namespace eCommerce.Models;

/// <summary>
/// Represents a member in the book shop website.
/// </summary>
public class Member
{
    /// <summary>
    /// The unique identifier for the member.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// The username of the member.
    /// </summary>
    public required string Username { get; set; }

    /// <summary>
    /// The password of the member.
    /// </summary>
    public required string Password { get; set; }

    /// <summary>
    /// The email address of the member.
    /// </summary>
    public required string Email { get; set; }
}
