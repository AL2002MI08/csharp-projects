using System.ComponentModel.DataAnnotations;

namespace GameCatalogApi.Models;

public record LoginRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; init; } = string.Empty;

    [Required]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*[\W_]).+$", ErrorMessage = "Password must contain at least one uppercase letter and a special character")]
    public string Password { get; init; } = string.Empty;
}