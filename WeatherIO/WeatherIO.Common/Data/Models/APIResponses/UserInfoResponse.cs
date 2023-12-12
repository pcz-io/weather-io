using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherIO.Common.Data.Models.APIResponses
{
    /// <summary>
    /// This class represents json data returned from backend endpoint `get-user-info`.
    /// </summary>
    public class UserInfoResponse
    {
        /// <summary>
        /// User username
        /// </summary>
        public string UserName {  get; set; }
    }
}
