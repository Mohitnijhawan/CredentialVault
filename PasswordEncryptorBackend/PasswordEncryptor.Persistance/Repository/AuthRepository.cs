using System;
using System.Collections.Generic;
using System.Text;
using PasswordEncryptor.Application.Abstraction.IRepository;
using PasswordEncryptor.Domain.Entities;
using PasswordEncryptor.Persistance.Data;

namespace PasswordEncryptor.Persistance.Repository
{
    public class AuthRepository(PasswordEncryptorDbContext context):BaseRepository<User>(context),IAuthRepository
    {
    }
}
