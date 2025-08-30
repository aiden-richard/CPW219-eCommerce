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
    [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Username must be alphanumeric")]
    [StringLength(30, MinimumLength = 1, ErrorMessage = "Username must be between 1 and 30 characters")]
    public required string Username { get; set; }

    /// <summary>
    /// The password of the member.
    /// </summary>
    [StringLength(50, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 50 characters")]
    public required string Password { get; set; }

    /// <summary>
    /// The email address of the member.
    /// </summary>
    [EmailAddress]
    public required string Email { get; set; }

    /// <summary>
    /// The date of birth of the member.
    /// </summary>
    [DataType(DataType.Date)]
    public DateOnly DateOfBirth { get; set; }

}

public class RegistrationViewModel
{
    /// <summary>
    /// The username of the member.
    /// </summary>
    [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Username must be alphanumeric")]
    [StringLength(30, MinimumLength = 1, ErrorMessage = "Username must be between 1 and 30 characters")]
    public required string Username { get; set; }

    /// <summary>
    /// The password of the member.
    /// </summary>
    [StringLength(50, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 50 characters")]
    public required string Password { get; set; }

    /// <summary>
    /// The confirmed password of the member.
    /// </summary>
    [StringLength(50, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 50 characters")]
    [Compare(nameof(Password))]
    [Display(Name = "Confirm Password")]
    public required string ConfirmPassword { get; set; }

    /// <summary>
    /// The email address of the member.
    /// </summary>
    [EmailAddress]
    public required string Email { get; set; }

    /// <summary>
    /// The date of birth of the member.
    /// </summary>
    [DataType(DataType.Date)]
    public DateOnly DateOfBirth { get; set; }
}

public class LoginViewModel
{
    /// <summary>
    /// The username of the member.
    /// </summary>
    public required string UsernameOrEmail { get; set; }

    /// <summary>
    /// The password of the member.
    /// </summary>
    [StringLength(50, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 50 characters")]
    public required string Password { get; set; }
}