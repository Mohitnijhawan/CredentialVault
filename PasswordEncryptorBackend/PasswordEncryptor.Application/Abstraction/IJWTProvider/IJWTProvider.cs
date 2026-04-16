using System;
using System.Collections.Generic;
using System.Text;
using PasswordEncryptor.Domain.Entities;

namespace PasswordEncryptor.Application.Abstraction.IJWTProvider
{
    public interface IJWTProvider
    {
        string GenerateAuthToken(User user);

        string GenerateRefreshToken();
    }
}
