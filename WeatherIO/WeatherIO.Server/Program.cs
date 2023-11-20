using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WeatherIO.Server.Data.Models;
using WeatherIO.Server.Data.Services;

namespace WeatherIO.Server
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var dbhost = Environment.GetEnvironmentVariable("DB_HOST");
            var dbname = Environment.GetEnvironmentVariable("DB_NAME");
            var dbpassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");
            var connectionString = $"Data Source={dbhost};Initial Catalog={dbname};User ID=sa;Password={dbpassword};TrustServerCertificate=True";
            if (dbhost == null || dbname == null || dbpassword == null)
                connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=WeatherDBDev;Integrated Security=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:ValidAudience"],
                    ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]!)),
                    RequireExpirationTime = false
                };
            });
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            builder.Services.AddHttpClient();
            builder.Services.AddScoped<GeocodingService>();
            builder.Services.AddScoped<ForecastService>();
            builder.Services.AddScoped<AirQualityService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                using var srvc = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
                var context = srvc.ServiceProvider.GetService<ApplicationDbContext>();
                context!.Database.Migrate();
            }

            app.UseCors(options => options.SetIsOriginAllowed(o => true).AllowAnyHeader().AllowAnyMethod().AllowCredentials());

            app.UseRouting();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
