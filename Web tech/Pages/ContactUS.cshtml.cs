using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Web_tech.Pages
{
    [Authorize]
    public class ContactUSModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
