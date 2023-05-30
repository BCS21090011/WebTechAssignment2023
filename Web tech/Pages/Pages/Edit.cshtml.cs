using Data.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Web_tech.Pages.Pages
{
    public class EditModel : PageModel
    {
        private readonly DatabaseContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        [BindProperty]
        public Questions Question { get; set; }

        [BindProperty]
        public List<Subjects> Subjects { get; set; }

        [BindProperty]
        public IFormFile? questionImage { get; set; }

        [BindProperty]
        public IFormFile? answerImage { get; set; }

        public EditModel(IWebHostEnvironment webHostEnvironment, DatabaseContext context)
        {
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }

        public async Task<IActionResult> OnGet(int? id)
        {
            Subjects = _context.Subjects.ToList();

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

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Question.QuestionImageFileName = HandleImgFile(questionImage);
            Question.AnswerImageFileName = HandleImgFile(answerImage);

            _context.Attach(Question).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionExists(Question.QuestionsId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./CRUD");
        }

        private bool QuestionExists(int id)
        {
            return _context.Questions.Any(q => q.QuestionsId == id);
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
