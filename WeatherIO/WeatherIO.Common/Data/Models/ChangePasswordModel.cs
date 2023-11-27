using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherIO.Common.Data.Models
{
    /// <summary>
    /// This is a class model used by `change-password` endpoint.
    /// </summary>
    public class ChangePasswordModel
    {
        /// <summary>
        /// Old password of the user that wants to change password.
        /// </summary>
        public string OldPassword { get; set; } = string.Empty;

        /// <summary>
        /// New password for user changing password.
        /// </summary>
        public string NewPassword { get; set; } = string.Empty;
    }
}
