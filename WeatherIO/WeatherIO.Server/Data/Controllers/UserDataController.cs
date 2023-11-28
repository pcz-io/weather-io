using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeatherIO.Common.Data.Models;

namespace WeatherIO.Server.Data.Controllers
{
    /// <summary>
    /// This class contains endpoints related to user data
    /// </summary>
    [Authorize]
    [Route("api")]
    public class UserDataController : Controller
    {
        /// <summary>
        /// Dependency injected database context
        /// </summary>
        private readonly ApplicationDbContext _dbContext;

        /// <summary>
        /// Constructor with dependency injected database context
        /// </summary>
        /// <param name="dbContext">Databease context</param>
        public UserDataController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// This method handles requests to the add-update-favourite-location endpoint
        /// </summary>
        /// <param name="model">Request body</param>
        /// <returns>FavouriteLocationModel with status 200 Ok if no errors ocured, otherwise 400 BadRequest</returns>
        [HttpPost]
        [Route("add-update-favourite-location")]
        public async Task<IActionResult> AddOrUpdateFavouriteLocation([FromBody] FavouriteLocationModel model)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity!.Name);
            if (user == null)
            {
                return BadRequest();
            }

            _dbContext.FavouriteLocations.Update(new()
            {
                Id = model.Id,
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                Name = model.Name,
                User = user
            });
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        /// <summary>
        /// This method handles requests to the add-update-favourite-location endpoint
        /// </summary>
        /// <param name="model">Request body</param>
        /// <returns>Status 200 Ok if no errors ocured, otherwise 400 BadRequest</returns>
        [HttpPost]
        [Route("delete-favourite-location")]
        public async Task<IActionResult> DeleteFavouriteLocation([FromBody] FavouriteLocationModel model)
        {
            var user = await _dbContext.Users.Include(u => u.FavouriteLocations).FirstOrDefaultAsync(u => u.UserName == User.Identity!.Name);
            if (user == null)
            {
                return BadRequest();
            }

            var favLocation = user.FavouriteLocations.FirstOrDefault(f => f.Id == model.Id);
            if (favLocation == null)
            {
                return BadRequest();
            }
            _dbContext.FavouriteLocations.Remove(favLocation);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        /// <summary>
        /// This method handles requests to the get-favourite-location endpoint
        /// </summary>
        /// <returns>JSON array of FavouriteLocationModel with status 200 Ok if no errors ocured, otherwise 400 BadRequest</returns>
        [HttpGet]
        [Route("get-favourite-locations")]
        public async Task<IActionResult> GetFavouriteLocations()
        {
            var user = await _dbContext.Users.Include(u => u.FavouriteLocations).FirstOrDefaultAsync(u => u.UserName == User.Identity!.Name);
            if (user == null)
            {
                return BadRequest();
            }

            return Ok(user.FavouriteLocations.Select(f =>
            {
                return new FavouriteLocationModel
                {
                    Latitude = f.Latitude,
                    Longitude = f.Longitude,
                    Id = f.Id,
                    Name = f.Name
                };
            }));
        }
    }
}
