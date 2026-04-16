using System;
using System.Collections.Generic;
using System.Text;
using PasswordEncryptor.Application.Abstraction.IRepository;
using PasswordEncryptor.Application.RRModel.Credential;
using PasswordEncryptor.Domain.Entities;
using PasswordEncryptor.Persistance.Data;

namespace PasswordEncryptor.Persistance.Repository
{
    public class CredentialRepository(PasswordEncryptorDbContext context) : BaseRepository<Credential>(context), ICredentialRepository
    {
        public async Task<IEnumerable<CredentialResponse>> GetAllCredentials()
        {
                                    var query = $@"Select Id,Title
                        ,Username,EncryptedPassword,CreatedAt
                        from Credentials";

            return await QueryAsync<CredentialResponse>(query);
        }


        public async Task<IEnumerable<CredentialResponse>> GetCredentialsByUserNameAndTitle(string? username, string? title)
        {
                                var query = $@"SpGetCredentialsFiltered";
            return await QueryAsync<CredentialResponse>(query,new {username,title},commandType:System.Data.CommandType.StoredProcedure);
        }
    }
}
