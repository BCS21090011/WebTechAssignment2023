using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;



namespace Web_tech.Pages
{
    public class study_loginModel : PageModel
    {
        [BindProperty]
        public string? Username { get; set; }

        [BindProperty]
        public string? Password { get; set; }

        public string? Message { get; set; }

        private readonly IConfiguration _configuration;

        public study_loginModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnGet()
        {
            // This method is executed when the page is loaded
        }

        public IActionResult OnPost()
        {
            string connectionString = "data source=DESKTOP-M3I2KTO;initial catalog=WebTechAssignmentDB;trusted_connection=true;";
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
                        // Generate a JWT token with a user claims
                        var tokenHandler = new JwtSecurityTokenHandler();
                        var key = Encoding.UTF8.GetBytes(_configuration["JwtKey"]);
                        var tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(new Claim[]
                            {
                                new Claim(ClaimTypes.Name, Username),
                                new Claim(ClaimTypes.Role, "User")
                            }),
                            Expires = DateTime.UtcNow.AddDays(7),
                            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                        };
                        var token = tokenHandler.CreateToken(tokenDescriptor);
                        var jwtToken = tokenHandler.WriteToken(token);

                        // Return the JWT token
                        return new JsonResult(new { Token = jwtToken });
                     

                        
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
