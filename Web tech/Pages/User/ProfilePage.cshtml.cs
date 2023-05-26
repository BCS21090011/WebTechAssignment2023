using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace Web_tech.Pages.Pages
{
    [Authorize(Roles = "Admin")]
    public class ProfilePageModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}