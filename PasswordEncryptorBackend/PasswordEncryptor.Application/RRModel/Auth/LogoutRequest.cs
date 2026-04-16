using System;
using System.Collections.Generic;
using System.Text;

namespace PasswordEncryptor.Application.RRModel.Auth
{
    public class LogoutRequest
    {
        public string RefreshToken { get; set; }
    }
}
