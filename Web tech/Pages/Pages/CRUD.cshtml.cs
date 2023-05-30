using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Web_tech.Pages.Pages
{
    public class CRUDModel : PageModel
    {
        private readonly DatabaseContext _context;

        public CRUDModel(DatabaseContext context)
        {
            _context = context;
        }

        [BindProperty]
        public IList<Questions> Questions { get; set; }

        public async Task<IActionResult> OnGet()
        {
            Questions = await _context.Questions.ToListAsync();
            if (Questions == null)
            {
                Questions = new List<Questions>(); // Initialize the collection if it's null
            }
            return Page();
        }

        [BindProperty]
        public Questions Question { get; set; }

        public async Task<IActionResult> OnGetEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Question = await _context.Questions.FindAsync(id);

            if (Question == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostEditAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionToUpdate = await _context.Questions.FindAsync(id);

            if (questionToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Questions>(
                questionToUpdate,
                "Question",
                q => q.Question, q => q.QuestionMark, q => q.QuestionDifficulty,
                q => q.SubjectsId, q => q.QuestionImageFileName, q => q.Answer,
                q => q.AnswerImageFileName))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("CRUD");
            }

            return Page();
        }
        
        public async Task<IActionResult> OnGetDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Question = await _context.Questions.FindAsync(id);

            if (Question == null)
            {
                return NotFound();
            }

            return Page();
        }
        [HttpPost]
        public async Task<IActionResult> OnPostDeleteAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionToDelete = await _context.Questions.FindAsync(id);

            if (questionToDelete == null)
            {
                return NotFound();
            }

            _context.Questions.Remove(questionToDelete);
            await _context.SaveChangesAsync();

            return RedirectToPage("CRUD");
        }

    }
}
