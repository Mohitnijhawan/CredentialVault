using System;
using System.Collections.Generic;
using System.Text;

namespace PasswordEncryptor.Application.Abstraction.AppEncryption
{
    public interface IAppEncryption
    {
        string GenerateSalt();

        string HashPassword(string password,string salt);
    }
}
