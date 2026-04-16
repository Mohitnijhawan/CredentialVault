using System;
using System.Collections.Generic;
using System.Text;
using PasswordEncryptor.Application.RRModel.Credential;
using PasswordEncryptor.Domain.Entities;

namespace PasswordEncryptor.Application.Abstraction.IRepository
{
    public interface ICredentialRepository:IBaseRepository<Credential>
    {
        Task<IEnumerable<CredentialResponse>> GetAllCredentials();

        Task<IEnumerable<CredentialResponse>> GetCredentialsByUserNameAndTitle(string? username, string? title);
    }
}
