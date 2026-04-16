using Microsoft.Extensions.DependencyInjection;
using PasswordEncryptor.Application.Abstraction.IServices;
using PasswordEncryptor.Application.Services;

namespace PasswordEncryptor.Application
{
    public static class AssemblyReference
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IEncryptionService,EncryptionService>();
            services.AddAutoMapper(cfg => { }, typeof(AssemblyMarker.AssemblyMarker));
            services.AddScoped<ICredentialService,CredentialService>();
            return services;
        }
    }
}
