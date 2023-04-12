using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;


namespace Web_tech.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public string? Username { get; set; }

        [BindProperty]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [BindProperty]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match.")]
        public string? ConfirmPassword { get; set; }

        [BindProperty]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

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
                string connectionString = "data source=GJohnsonPC\\MYMSSQLSERVER;initial catalog=WebTechAssignmentDB;trusted_connection=true;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Check if username or email already exist in the database
                    string checkQuery = "SELECT COUNT(*) FROM Users WHERE UserName = @Username OR UserEmail = @Email";
                    SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
                    checkCommand.Parameters.AddWithValue("@Username", Username);
                    checkCommand.Parameters.AddWithValue("@Email", Email);
                    int count = (int)checkCommand.ExecuteScalar();
                    if (count > 0)
                    {
                        Message = "Username or email already exists.";
                        return Page();
                    }

                    // Insert new user into the database
                    string insertQuery = "INSERT INTO Users (UserName, UserPassword, UserEmail) VALUES (@Username, @Password, @Email)";
                    SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                    insertCommand.Parameters.AddWithValue("@Username", Username);
                    insertCommand.Parameters.AddWithValue("@Password", Password);
                    insertCommand.Parameters.AddWithValue("@Email", Email);
                    insertCommand.ExecuteNonQuery();

                    Message = "Registration successful!";
                }
            }
            return Page();
        }

        private bool IsValid()
        {
            // perform validation on the inputs
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(Email))
            {
                Message = "Please fill in all required fields.";
                return false;
            }
            if (Password != ConfirmPassword)
            {
                Message = "Passwords do not match.";
                return false;
            }
            if (!IsValidEmail(Email))
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
