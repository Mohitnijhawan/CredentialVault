using System;
using System.Collections.Generic;
using System.Text;

namespace PasswordEncryptor.Application.RRModel.Auth
{
    public class SignUpRequest
    {
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string ContactNo {  get; set; } = string.Empty;
    }
}
