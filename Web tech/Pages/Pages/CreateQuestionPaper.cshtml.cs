using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using System.Data;

namespace Web_tech.Pages
{
    [Authorize(Roles = "Admin")]
    public class CreateQuestionPaperModel : PageModel
    {
        private readonly DatabaseContext? _context;

        [BindProperty]
        public List<Questions>? Qsts { get; set; }

        [BindProperty]
        public List<Subjects>? Subjects { get; set; }

        public CreateQuestionPaperModel(DatabaseContext? context)
        {
            _context = context;
        }

        public void OnGet()
        {
            // Get the subjects:
            Subjects = _context.Subjects.ToList();

            // Get the questions:
            Qsts = _context.Questions.ToList();
            /*

            // Testing samples:
            Qsts = new List<Questions>();

            for (int i = 0; i < 5; i++)
            {
                Questions question = new Questions
                {
                    QuestionsId = i + 1,
                    Question = $"Question {i + 1}",
                    QuestionMark = i + 1,
                    QuestionDifficulty = i + 1,
                    SubjectsId = i + 1,
                    QuestionImageFileName = null,
                    Answer = $"Answer {i + 1}",
                    AnswerImageFileName = null
                };

                Qsts.Add(question); */
        }
    }

}