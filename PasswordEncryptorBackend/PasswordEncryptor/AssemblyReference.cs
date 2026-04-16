using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Carter;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PasswordEncryptor.Application;
using PasswordEncryptor.InfraStructure;
using PasswordEncryptor.Persistance;

namespace PasswordEncryptor
{
    public static class AssemblyReference
    {
        public static IServiceCollection AddApiService(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            services.AddOpenApi();
            services.AddCors();
            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails();
            services.AddCarter();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme=JwtBearerDefaults.AuthenticationScheme;

                
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata=true;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer= true,
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidateAudience= true,
                    ValidAudience = configuration["JWT:Audience"],
                    ValidateIssuerSigningKey=true,
                    RequireExpirationTime=true,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey =new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]!))
                };
            });
            services.AddAuthorization();
            services.AddApplicationService().AddInfraStructureService().AddPersistanceService(configuration);
            return services;
        }
    }
}

//.AddGoogle(options =>
// {
//     options.ClientId = configuration["Google:ClientId"]!;
//     options.ClientSecret = configuration["Google:ClientSecret"]!;
//     options.CallbackPath = "/signin-google";

// }).AddCookie();