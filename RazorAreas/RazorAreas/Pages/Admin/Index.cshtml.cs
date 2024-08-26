using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using RazorAreas.Areas.Identity.Data.DbContexts;

namespace RazorAreas.Pages.Admin
{
    public class AdminPageModel : PageModel
    {
        private readonly UserContext _context;

        public AdminPageModel(UserContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnGetEditUser(string id)
        {
            return RedirectToPage("Manage/EditUser", new {id = id});
        }
        
        public async Task<IActionResult> OnGetDeleteUser(string id)
        {
            Console.WriteLine("Delete");
            return RedirectToPage("Index");

        }
        
        public async Task<IActionResult> OnGetGrantUser(string id)
        {
            Console.WriteLine("Grant");
            return RedirectToPage("Index");
        }
        
    }
}
