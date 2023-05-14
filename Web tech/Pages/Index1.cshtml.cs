using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Web_tech.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public string? UserName { get; set; }

        [BindProperty]
        [DataType(DataType.Password)]
        public string? UserPassword { get; set; }

        [BindProperty]
        [DataType(DataType.Password)]
        [Compare(nameof(UserPassword), ErrorMessage = "Passwords do not match.")]
        public string? ConfirmPassword { get; set; }

        [BindProperty]
        public string? UserGender { get; set; }

        [BindProperty]
        public string? UserCountry { get; set; }

        [BindProperty]
        [DataType(DataType.EmailAddress)]
        public string? UserEmail { get; set; }

        [BindProperty]
        public int UserPhoneNumber { get; set; }

        [BindProperty]
        public string? UserSchoolName { get; set; }

        public string? Message { get; set; }

        private readonly IConfiguration _config;

        public RegisterModel(IConfiguration config)
        {
            _config = config;
        }

        public void OnGet()
        {
            // Initial page load
        }

        public IActionResult OnPost()
        {
            // Form submission
            if (IsValid())
            {
                string connectionString = "data source=DESKTOP-M3I2KTO;initial catalog=WebTechAssignmentDB;trusted_connection=true;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Check if username or email already exist in the database
                    string checkQuery = "SELECT COUNT(*) FROM dbo.Users WHERE UserName = @Username OR UserEmail = @Email";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@Username", UserName);
                        checkCommand.Parameters.AddWithValue("@Email", UserEmail);
                        int count = (int)checkCommand.ExecuteScalar();

                        if (count > 0)
                        {
                            Message = "Username or email already exists.";
                            return Page();
                        }
                    }

                    // Insert new user into the database
                    string insertQuery = "INSERT INTO dbo.Users (UserName, UserPassword, UserGender, UserCountry, UserEmail, UserPhoneNumber, UserSchoolName) " +
                        "VALUES (@Username, @Password, @Gender, @Country, @Email, @PhoneNumber, @SchoolName)";
                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@Username", UserName);
                        insertCommand.Parameters.AddWithValue("@Password", UserPassword);
                        insertCommand.Parameters.AddWithValue("@Gender", UserGender);
                        insertCommand.Parameters.AddWithValue("@Country", UserCountry);
                        insertCommand.Parameters.AddWithValue("@Email", UserEmail);
                        insertCommand.Parameters.AddWithValue("@PhoneNumber", UserPhoneNumber);
                        insertCommand.Parameters.AddWithValue("@SchoolName", UserSchoolName);
                        insertCommand.ExecuteNonQuery();
                    }

                    Message = "Registration successful!";
                    Task.Delay(3000).Wait();
                    return RedirectToPage("/index");
                }
            }
            return Page();
        }

        private bool IsValid()
        {
            // perform validation on the inputs
            if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(UserPassword) || string.IsNullOrWhiteSpace(UserEmail))
            {
                Message = "Please fill in all required fields.";
                return false;
            }
            if (UserPassword != ConfirmPassword)
            {
                Message = "Passwords do not match.";
                return false;
            }
            if (!IsValidEmail(UserEmail))
            {
                Message = "Invalid email address.";
                return false;
            }
            return true;
        }

        private bool IsValidEmail(string email)
        {
            // perform email validation using regex or other methods
            // this is a simple example using a regular expression
            return new EmailAddressAttribute().IsValid(email);
        }
    }
}
