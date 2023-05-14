using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web_tech.Pages
{
    [Authorize]
    public class Index3Model : PageModel
    {
        public void OnGet()
        {
        }
    }
}
