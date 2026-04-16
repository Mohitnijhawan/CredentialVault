using System;
using System.Collections.Generic;
using System.Text;

namespace PasswordEncryptor.Application.Abstraction.IServices
{
    public interface IEncryptionService
    {
        string Encrypt(string plainText);
        string Decrypt(string cipherText);
    }
}
