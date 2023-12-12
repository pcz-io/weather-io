using System.ComponentModel.DataAnnotations;

namespace WeatherIO.Common.Data.Models
{
    /// <summary>
    /// Represents POST data sent to the `register` endpoint
    /// </summary>
    public class RegisterModel
    {
        /// <summary>
        /// Email address of the user
        /// </summary>
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        /// <summary>
        /// Users password
        /// </summary>
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
