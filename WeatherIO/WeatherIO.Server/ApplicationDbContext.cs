using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using WeatherIO.Server.Data.Models;

namespace WeatherIO.Server
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
		/// <summary>
		/// Represents database FavouriteLocations table
		/// </summary>
		public DbSet<FavouriteLocation> FavouriteLocations { get; set; }

        /// <summary>
        /// Database context constructor
        /// </summary>
        /// <param name="options"></param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// This method is used by entity framework to generate migration
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
