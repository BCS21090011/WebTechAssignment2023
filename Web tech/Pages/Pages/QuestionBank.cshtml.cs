using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace Web_tech.Pages
{
    // [Authorize(Roles = "Admin")]
    public class QuestionBankModel : PageModel
    {
        private readonly DatabaseContext? _context;

        [BindProperty]
        public List<Questions>? Qsts { get; set; }

        [BindProperty]
        public List<Subjects>? Subjects { get; set; }

        public QuestionBankModel(DatabaseContext? context)
        {
            _context = context;
        }
        public void OnGet()
        {
            Qsts = _context.Questions.ToList();
            Subjects = _context.Subjects.ToList();
        }
    }
}
