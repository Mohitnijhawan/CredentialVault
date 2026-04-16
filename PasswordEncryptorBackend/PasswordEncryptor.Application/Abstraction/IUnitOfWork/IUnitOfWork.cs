using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PasswordEncryptor.Application.Abstraction.IUnitOfWork
{
    public interface IUnitOfWork
    {
        IDbTransaction BeginTransaction();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken=default);
    }
}
