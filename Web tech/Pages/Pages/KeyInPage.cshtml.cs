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

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Get the data from the form
            string difficulty = Request.Form["difficulty"];
            string subject = Request.Form["subject"];
            string question = Request.Form["question"];
            string answer = Request.Form["answer"];
            int marks = Convert.ToInt32(Request.Form["marks"]);

            // Create a new Questions object
            var newQuestion = new Questions
            {
                Question = question,
                QuestionMark = marks,
                QuestionDifficulty = Convert.ToInt32(difficulty),
                SubjectsId = Convert.ToInt32(subject),
                QuestionImageFileName = "", // Set the value for the image file name if applicable
                Answer = answer,
                AnswerImageFileName = "" // Set the value for the answer image file name if applicable
            };

            // Add the new question to the database
            _context.Questions.Add(newQuestion);
            await _context.SaveChangesAsync();

            // Redirect to the desired page
            return RedirectToPage("/Index");
        }
    }
}
