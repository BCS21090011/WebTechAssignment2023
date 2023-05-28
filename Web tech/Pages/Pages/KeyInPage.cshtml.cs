// KeyInPage.cshtml.cs

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        /*
        [Display(Name = "Question Image")]
        public IFormFile questionImage { get; set; }

        [Display(Name = "Answer Image")]
        public IFormFile answerImage { get; set; }
        */

        public KeyInPageModel(DatabaseContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Questions? Questions { get; set; }

        [BindProperty]
        public List<Subjects>? Subjects { get; set; }

        public void OnGet()
        {
            // Get the subjects:
            Subjects = _context.Subjects.ToList();
        }
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                /*
                // Handle question image upload
                string? questionImageName = HandleImgFile(questionImage);

                // Handle answer image upload
                string? answerImageName = HandleImgFile(answerImage);

                // Save the image filenames in the database
                Questions.QuestionImageFileName = questionImageName;
                Questions.AnswerImageFileName = answerImageName;
                */

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

        private string? HandleImgFile(IFormFile imageFile)
        {
            if (imageFile != null)
            {
                string uniqueFileName = GetUniqueFileName(imageFile.FileName);
                string filePath = Path.Combine("/WareHouse/Images", uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    imageFile.CopyTo(stream);
                }

                return uniqueFileName;
            }

            return null;
        }

        private string GetUniqueFileName(string fileName)
        {
            string uniqueFileName = Path.GetFileNameWithoutExtension(fileName) + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + Path.GetExtension(fileName);

            return uniqueFileName;
        }

    }
}