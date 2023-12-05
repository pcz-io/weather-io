using System.ComponentModel.DataAnnotations;

namespace WeatherIO.Common.Data.Models
{
    /// <summary>
    /// Represents POST data sent to `login` endpoint
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// Email of the user
        /// </summary>
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        /// <summary>
        /// User password
        /// </summary>
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
