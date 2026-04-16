using System;
using System.Collections.Generic;
using System.Text;

namespace PasswordEncryptor.Application.RRModel.Auth
{
    public class LoginResponse
    {
        public Guid UserId {  get; set; }

        public string Email { get; set; } = string.Empty;

        public string AccessToken { get; set; } = string.Empty;

        public string RefreshToken { get; set; }
    }
}
