using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WeatherIO.Common.Data.Models;
using WeatherIO.Common.Data.Models.APIResponses;
using WeatherIO.Server.Data.Models;

namespace WeatherIO.Server.Data.Controllers
{
    /// <summary>
    /// This class is responsible for handling authentication endpoints such as
    /// `login`, `register`, `delete-account`, `change-password` and `get-user-info`.
    /// </summary>
    [Route("api")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        /// <summary>
        /// Dependency injected UserManager service
        /// </summary>
        private readonly UserManager<ApplicationUser> userManager;

        /// <summary>
        /// Dependency injected RoleManager service
        /// </summary>
        private readonly RoleManager<IdentityRole> roleManager;

        /// <summary>
        /// Dependency injected IConfiguration service
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructor with dependency injected service
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="roleManager"></param>
        /// <param name="configuration"></param>
        public AuthenticateController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
        }

        /// <summary>
        /// This method handles `login` endpoint.
        /// </summary>
        /// <param name="model">Input POST data</param>
        /// <returns>
        /// Status 200 Ok with LoginResponse as response body if succedeed.
        /// Otherwise 401 Unauthorized
        /// </returns>
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await userManager.FindByNameAsync(model.Email);
            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new LoginResponse()
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
            return Unauthorized();
        }

        /// <summary>
        /// This method handles `register` endpoint.
        /// </summary>
        /// <param name="model">Input POST data</param>
        /// <returns>
        /// Status 200 Ok with RegistrationResponse as response body if succedeed.
        /// Otherwise 500 InternalServerError
        /// </returns>
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.Email);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new RegistrationResponse { Status = "Error", Message = "Użytkownik z podanym adresem e-mail już istnieje!" });

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Email
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new RegistrationResponse { Status = "Error", Message = "Nie udało się utowrzyć konta! Sprawdź wprowadzone dane i spróbuj ponownie." });

            return Ok(new RegistrationResponse { Status = "Success", Message = $"Użytkownik {model.Email} utworzony!" });
        }

        /// <summary>
        /// This method handles `delete-account` endpoint
        /// </summary>
        /// <returns>Status 200 Ok if succedeed, otherwise 401 Unauthorized</returns>
        [HttpPost]
        [Authorize]
        [Route("delete-account")]
        public async Task<IActionResult> DeleteAccount()
        {
            var user = await userManager.FindByNameAsync(User.Identity!.Name);
            if (user == null)
                return Unauthorized();
            await userManager.DeleteAsync(user);
            return Ok();
        }

        /// <summary>
        /// This function handles `change-password` endpoint.
        /// </summary>
        /// <param name="model">Input data for the endpoint</param>
        /// <returns>Status 200 Ok or 400 Bad Request when input data is invalid.</returns>
        [HttpPost]
        [Authorize]
        [Route("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
        {
            var user = await userManager.FindByNameAsync(User.Identity!.Name!);
            if (user == null)
                return Unauthorized();
            var result = await userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (result.Succeeded)
                return Ok();
            else
                return BadRequest(result.Errors);
        }

        /// <summary>
        /// This function handles `get-user-info` endpoint.
        /// </summary>
        /// <returns>Status 200 Ok with UserInfoResponse as body</returns>
        [HttpGet]
        [Authorize]
        [Route("get-user-info")]
        public IActionResult GetUserInfo()
        {
            return Ok(new UserInfoResponse
            {
                UserName = User.Identity!.Name ?? "UNKNOWN"
            });
        }
    }
}
