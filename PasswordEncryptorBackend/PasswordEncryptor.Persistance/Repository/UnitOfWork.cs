using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Microsoft.EntityFrameworkCore.Storage;
using PasswordEncryptor.Application.Abstraction.IUnitOfWork;
using PasswordEncryptor.Persistance.Data;

namespace PasswordEncryptor.Persistance.Repository
{
    public class UnitOfWork(PasswordEncryptorDbContext context) : IUnitOfWork
    {
        public IDbTransaction BeginTransaction()
        {
           return context.Database.BeginTransaction().GetDbTransaction();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await context.SaveChangesAsync(cancellationToken);
        }
    }
}
