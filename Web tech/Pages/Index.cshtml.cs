using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Web_tech.Pages
{
    public class study_loginModel : PageModel
    {
        [BindProperty]
        public string? Username { get; set; }

        [BindProperty]
        public string? Password { get; set; }

        public string? Message { get; set; }

        public void OnGet()
        {
            // This method is executed when the page is loaded
        }

        public IActionResult OnPost()
        {
            string connectionString = "data source=DESKTOP-M3I2KTO;initial catalog=Mydb;trusted_connection=true;";
            string queryString = "SELECT * FROM Users WHERE UserName=@UserName AND UserPassword=@UserPassword";

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(queryString, connection);

            {
                command.Parameters.AddWithValue("@UserName", Username);
                command.Parameters.AddWithValue("@UserPassword", Password);

                connection.Open();
                try
                {
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        // Redirect to the Dashboard page if login is successful
                        return RedirectToPage("/index3");
                    }
                    else
                    {
                        Message = "Invalid username or password";
                    }
                }
                catch (Exception errorMessage)
                {
                    Console.WriteLine("Error!\nError message:{\n" + errorMessage.Message + "\n}");
                }
            }

            return Page();
        }
    }
}
    
