using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProjectManagementTool2.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string UserId { get; set; }

        public IActionResult OnPost()
        {
            if (Guid.TryParse(UserId, out Guid userId))
            {
                TempData["UserId"] = userId.ToString();
                return RedirectToPage("/MainOverview");
            }
            else
            {
                ModelState.AddModelError("UserId", "Invalid GUID format");
                return Page();
            }
        }

        public IActionResult OnGet()
        {
            // You can perform additional logic here if needed
            return Page();
        }
    }
}