// KeyInPage.cshtml.cs

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web_tech.Pages
{
    [Authorize(Roles = "Admin")]
    public class KeyInPageModel : PageModel
    {

        private readonly DatabaseContext _context;

        public KeyInPageModel(DatabaseContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Questions? Questions { get; set; }

        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {


                // Save the data to the database
                await _context.Questions.AddAsync(Questions);
                await _context.SaveChangesAsync();
                Console.WriteLine("Success");


                return RedirectToPage("/Pages/Keyinpage");
            }
            else
            {
                return Page();
            }
        }

    }
}


