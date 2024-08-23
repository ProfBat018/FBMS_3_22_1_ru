
using System.ComponentModel.DataAnnotations;

namespace RazorAreas.Areas.Identity.Data.DTOs;

public class ForgotPasswordInputModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}
