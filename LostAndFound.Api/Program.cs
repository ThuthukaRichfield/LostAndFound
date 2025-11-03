using Intent.RoslynWeaver.Attributes;
using LostAndFound.Api.Configuration;
using LostAndFound.Api.Filters;
using LostAndFound.Application;
using LostAndFound.Application.Common.Interfaces;
using LostAndFound.Infrastructure;
using LostAndFound.Infrastructure.Identity; // Assuming ApplicationUser is here
using LostAndFound.Infrastructure.Persistence;
using LostAndFound.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens; // Required for SecurityKey
using System.Text;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.AspNetCore.Program", Version = "1.0")]

namespace LostAndFound.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Define the token signing key
            var signingKey = builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("Jwt:Key not found in configuration.");

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey))
                };
            });

            // 1. Existing DbContext registration
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                // 💡 REPLACE with your actual database provider and connection string
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // 2. Identity service registration (links to the DbContext above)
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => // <-- CHANGE 1: Use AddIdentity and add IdentityRole
            {
                options.SignIn.RequireConfirmedAccount = false;
                // You can also configure password, lockout, etc., options here
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            // Add services to the container.
            builder.Services.AddControllers(
                opt =>
                {
                    opt.Filters.Add<ExceptionFilter>();
                });
            builder.Services.AddApplication(builder.Configuration); 
            builder.Services.ConfigureApplicationSecurity(builder.Configuration);
            builder.Services.ConfigureHealthChecks(builder.Configuration);
            builder.Services.ConfigureProblemDetails();
            builder.Services.ConfigureApiVersioning();
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.ConfigureSwagger(builder.Configuration);

            // DI
            builder.Services.AddScoped<IIdentityService, IdentityService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseExceptionHandler();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication(); // ⚠️ MUST be BEFORE UseAuthorization
            app.UseAuthorization();
            app.MapControllers(); // ⚠️ MUST be AFTER UseAuthorization
            app.MapDefaultHealthChecks();
            app.UseSwashbuckle(builder.Configuration);

            app.Run();
        }
    }
}