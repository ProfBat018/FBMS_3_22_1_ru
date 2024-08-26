using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorAreas.Areas.Identity.Data.DbContexts;
using RazorAreas.Areas.Identity.Data.Models;

namespace RazorAreas.Pages.Admin.Manage;

public class EditUser : PageModel
{
    public AppUser User { get; set; }
    
    private readonly UserContext _context;

    public EditUser(UserContext context)
    {
        _context = context;
    }

    public void OnGet(string id)
    {
        User = _context.Users.Find(id);
    }

    public async Task OnPost(string userName, string id)
    {
        User = _context.Users.Find(id);

        User.UserName = userName;
        
        await _context.SaveChangesAsync();
    }
}

