using Carter;
using PasswordEncryptor.Application.Abstraction.IServices;
using PasswordEncryptor.Application.RRModel.Credential;
using PasswordEncryptor.Application.Services;

namespace PasswordEncryptor.EndPoints
{
    public class Credential : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder appbuilder)
        {
            var app = appbuilder.MapGroup("api/credentials").WithTags("credentials").RequireAuthorization();

            app.MapPost("", async (ICredentialService credentialService,CredentialRequest model) =>
            {
                return await credentialService.AddCredential(model);
            }).DisableAntiforgery();

            
            app.MapGet("", async (ICredentialService credentialService) =>
            {
                return await credentialService.GetCredentials();
            }).DisableAntiforgery();


            app.MapGet("{id:guid}", async (ICredentialService credentialService,Guid id) =>
            {
                return await credentialService.GetCredentialById(id);

            }).DisableAntiforgery();

            app.MapPut("", async (ICredentialService credentialService, CredentialUpdateRequest model) =>
            {
                return await credentialService.UpdateCredential(model);

            }).DisableAntiforgery();

            app.MapDelete("{id:guid}", async (ICredentialService credentialService, Guid id) =>
            {
                return await credentialService.DeleteCredential(id);

            }).DisableAntiforgery();

            app.MapGet("reveal/{id:guid}", async (ICredentialService credentialService, Guid id) =>
            {
                return await credentialService.RevealPassword(id);

            }).DisableAntiforgery();

            app.MapGet("search", async (ICredentialService credentialService,string? username,string? title) =>
            {
                return await credentialService.GetCredentialByUsernameandTitle(username, title);

            }).DisableAntiforgery();


        }
    }
}
