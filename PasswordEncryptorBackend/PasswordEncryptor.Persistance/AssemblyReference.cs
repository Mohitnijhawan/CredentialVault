using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PasswordEncryptor.Application.Abstraction.IRepository;
using PasswordEncryptor.Application.Abstraction.IUnitOfWork;
using PasswordEncryptor.Persistance.Data;
using PasswordEncryptor.Persistance.Repository;

namespace PasswordEncryptor.Persistance
{
    public static class AssemblyReference
    {
        public static IServiceCollection AddPersistanceService(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<PasswordEncryptorDbContext>(options =>
            {
                 options.UseSqlServer(configuration.GetConnectionString(nameof(PasswordEncryptorDbContext)));
            });
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<ICredentialRepository, CredentialRepository>();
            return services;
        }
    }
}
