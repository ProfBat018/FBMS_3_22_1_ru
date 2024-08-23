#nullable disable

using System.ComponentModel.DataAnnotations;

namespace RazorAreas.Areas.Identity.Data.DTOs;

public class LoginInputModel
{

    [Required]
    [EmailAddress]
    public string Email { get; set; }


    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }


    [Display(Name = "Remember me?")]
    public bool RememberMe { get; set; }
}
