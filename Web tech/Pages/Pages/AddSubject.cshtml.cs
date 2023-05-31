using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Web_tech.Pages.Pages
{
    [Authorize(Roles = "SuperAdmin")]
    public class AddSubjectModel : PageModel
    {
        private readonly DatabaseContext _context;

        [BindProperty]
        public Subjects Subject { get; set; }

        [BindProperty]
        public List<Subjects> Subjects { get; set; }

        public AddSubjectModel(DatabaseContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            Subjects = _context.Subjects.ToList();
        }

        public async Task<IActionResult> OnPost()
        {
            // Save the data into the database:
            if (CheckIfSubjectExists(Subject.SubjectName) == false)
            {
                await _context.Subjects.AddAsync(Subject);
                await _context.SaveChangesAsync();

                return RedirectToPage("/Pages/QuestionBank");
            }

            return Page();
        }

        public bool CheckIfSubjectExists(string subjectName)
        {
            bool exists = _context.Subjects.Any(s => s.SubjectName == subjectName);
            return exists;
        }
    }
}
