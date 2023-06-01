using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Web_tech.Pages.Pages
{
    [Authorize(Roles = "User")]
    public class ProfilePageModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        public ProfilePageModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public string? UserName { get; set; }

        [BindProperty]
        public string? Email { get; set; }

        [BindProperty]
        public string? PhoneNumber { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            // Set the initial values for the profile form
            UserName = user.UserName;
            Email = user.Email;
            PhoneNumber = user.PhoneNumber;

            // Pass the user details to the view for display
            ViewData["UserName"] = UserName;
            ViewData["Email"] = Email;
            ViewData["PhoneNumber"] = PhoneNumber;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            // Update the user's profile information
           // user.UserName = UserName;
            user.Email = Email;
            user.PhoneNumber = PhoneNumber;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                // Handle the update failure, e.g., display error messages
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return Page();
            }

            // Redirect back to the profile page
            return RedirectToPage("/user/profilepage");
        }
    }
}
