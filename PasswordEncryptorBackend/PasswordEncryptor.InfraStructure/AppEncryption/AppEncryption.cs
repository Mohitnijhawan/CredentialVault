using System;
using System.Collections.Generic;
using System.Text;
using BCrypt.Net;
using Microsoft.AspNetCore.Http;
using PasswordEncryptor.Application.Abstraction.AppEncryption;

namespace PasswordEncryptor.InfraStructure.AppEncryption
{
    public class AppEncryption : IAppEncryption
    {
        public string GenerateSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt();
        }

        public string HashPassword(string password, string salt)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, salt);
        }
    }
}
