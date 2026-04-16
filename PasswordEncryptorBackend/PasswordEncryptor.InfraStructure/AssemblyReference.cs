using Microsoft.Extensions.DependencyInjection;
using PasswordEncryptor.Application.Abstraction.AppEncryption;
using PasswordEncryptor.Application.Abstraction.Identity;
using PasswordEncryptor.Application.Abstraction.IJWTProvider;
using PasswordEncryptor.InfraStructure.Identity;

namespace PasswordEncryptor.InfraStructure
{
    public static class AssemblyReference
    {
        public static IServiceCollection AddInfraStructureService(this IServiceCollection services)
        {
            services.AddScoped<IAppEncryption, AppEncryption.AppEncryption>();
            services.AddScoped<IJWTProvider, JWTProvider.JWTProvider>();
            services.AddScoped<IHttpContextService, HttpContextService>();
            return services;
        }
    }
}
