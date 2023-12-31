﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherIO.Common.Data.Models;

namespace WeatherIO.Common.Data.Interfaces
{
    /// <summary>
    /// This is an interface for service that handles authentication with the server
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Logins to the server.
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns>True if succeeded otherwise false</returns>
        Task<bool> Login(LoginModel loginModel);

        /// <summary>
        /// Registers user
        /// </summary>
        /// <param name="registerModel"></param>
        /// <returns></returns>
        Task<bool> Register(RegisterModel registerModel);

        /// <summary>
        /// Logs out user, i.e. deletes Jwt token from storage.
        /// </summary>
        /// <returns></returns>
        Task Logout();

        /// <summary>
        /// Gets logged user username.
        /// </summary>
        /// <returns></returns>
        Task<string?> GetUsername();

        /// <summary>
        /// Deletes user account.
        /// </summary>
        /// <returns> True if succeeded otherwise false</returns>
        public Task<bool> DeleteAccount();

        /// <summary>
        /// Change user password.
        /// </summary>
        /// <param name="model"> Model containing old and new password.</param>
        /// <returns> True if succeeded otherwise false</returns>
        public Task<bool> ChangePassword(ChangePasswordModel model);
    }
}
