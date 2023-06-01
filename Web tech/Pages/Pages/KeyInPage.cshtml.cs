// KeyInPage.cshtml.cs

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web_tech.Pages
{
    [Authorize(Roles = "User")]
    public class KeyInPageModel : PageModel
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly DatabaseContext _context;

        public KeyInPageModel(IWebHostEnvironment webHostEnvironment, DatabaseContext context)
        {
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }

        [BindProperty]
        public Questions? Questions { get; set; }

        [BindProperty]
        public List<Subjects>? Subjects { get; set; }

        [BindProperty]
        public IFormFile? questionImage { get; set; }

        [BindProperty]
        public IFormFile? answerImage { get; set; }

        public void OnGet()
        {
            // Get the subjects:
            Subjects = _context.Subjects.ToList();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                Questions.QuestionImageFileName = HandleImgFile(questionImage);
                Questions.AnswerImageFileName = HandleImgFile(answerImage);

                // Save the data to the database
                await _context.Questions.AddAsync(Questions);
                await _context.SaveChangesAsync();
                Console.WriteLine("Success");

                return RedirectToPage("/Pages/QuestionBank");
            }
            else
            {
                return Page();
            }

        }

        private string? HandleImgFile(IFormFile? imageFile)
        {
            if (imageFile != null)
            {
                // Get the web root path
                var webRootPath = _webHostEnvironment.WebRootPath;

                // Specify the folder path to save the image
                var imagePath = Path.Combine(webRootPath, "WareHouse", "Images");

                // Create the folder if it doesn't exist
                if (!Directory.Exists(imagePath))
                {
                    Directory.CreateDirectory(imagePath);
                }

                // Generate a unique file name
                var uniqueFileName = GetUniqueFileName(imageFile.FileName);

                // Combine the folder path and unique file name
                var filePath = Path.Combine(imagePath, uniqueFileName);

                // Save the image to the specified folder
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    imageFile.CopyTo(stream);
                }

                return "/WareHouse/Images/" + uniqueFileName;
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


