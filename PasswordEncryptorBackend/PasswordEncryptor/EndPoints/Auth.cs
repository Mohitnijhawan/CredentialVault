using System.Security.Claims;
using Carter;
using Microsoft.AspNetCore.Authentication;
using PasswordEncryptor.Application.Abstraction.IServices;
using PasswordEncryptor.Application.RRModel.Auth;

namespace PasswordEncryptor.EndPoints
{
    public class Auth : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder appbuilder)
        {
            var app = appbuilder.MapGroup("api/auth").WithTags("auth");
            app.MapPost("sign-up", async (IAuthService authService,SignUpRequest model) =>
            {
                return await authService.SignUp(model);

            }).DisableAntiforgery();


            app.MapPost("login", async (IAuthService authService, LoginRequest model) =>
            {
                return await authService.Login(model);

            }).DisableAntiforgery();

            app.MapPost("refresh-token", async (IAuthService authService,RefreshTokenRequest model) =>
            {
                return await authService.RefreshToken(model);

            }).DisableAntiforgery();

            app.MapPost("logout", async (IAuthService authService) =>
            {
                    return await authService.LogOut();

            }).DisableAntiforgery();


        }



        
    }
}
