using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Web_tech.Pages.User
{
    [Authorize(Roles = "Admin")]
    public class ManageRolesModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ManageRolesModel(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public List<IdentityUser>? Users { get; set; }
        public List<IdentityRole>? Roles { get; set; }
        public string? SuccessMessage { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
            Users = await _userManager.Users.ToListAsync();
            Roles = await _roleManager.Roles.ToListAsync();

            return Page();
        }


        public async Task<IActionResult> OnPostAssignRoleAsync(string userId, string roleId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var role = await _roleManager.FindByIdAsync(roleId);

            if (user != null && role != null)
            {
                var isUserSuperAdmin = await _userManager.IsInRoleAsync(user, "SuperAdmin");

                if (isUserSuperAdmin && role.Name != "SuperAdmin")
                {
                    // If the user is a superadmin and the selected role is not superadmin,
                    // remove the superadmin role
                    var result = await _userManager.RemoveFromRoleAsync(user, "SuperAdmin");
                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                    else
                    {
                        SuccessMessage = "Role changed successfully. User is no longer a superadmin.";
                    }
                }

                var isUserInRole = await _userManager.IsInRoleAsync(user, role.Name);

                if (!isUserInRole)
                {
                    var result = await _userManager.AddToRoleAsync(user, role.Name);
                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                    else
                    {
                        SuccessMessage = "Role assigned successfully.";
                    }
                }
                else
                {
                    ModelState.AddModelError("", "User already has the selected role.");
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid user or role.");
            }

            Users = await _userManager.Users.ToListAsync();
            Roles = await _roleManager.Roles.ToListAsync();

            return Page();
        }
    }
}