using System;
using System.Collections.Generic;
using System.Text;
using PasswordEncryptor.Application.RRModel.Credential;
using PasswordEncryptor.Application.Utility;

namespace PasswordEncryptor.Application.Abstraction.IServices
{
    public interface ICredentialService
    {
        Task<Result<CredentialResponse>> AddCredential(CredentialRequest model);

        Task<Result<IEnumerable<CredentialResponse>>> GetCredentials();
        Task<Result<CredentialResponse>> GetCredentialById(Guid id);

        Task<Result<CredentialResponse>> UpdateCredential(CredentialUpdateRequest model);
        Task<Result<CredentialResponse>> DeleteCredential(Guid id);

        Task<Result<string>> RevealPassword(Guid id);

        Task<Result<IEnumerable<CredentialResponse>>> GetCredentialByUsernameandTitle(string? username,string? title);
    }
}
