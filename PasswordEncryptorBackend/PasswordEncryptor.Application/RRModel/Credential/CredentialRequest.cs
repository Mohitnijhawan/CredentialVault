    using System;
using System.Collections.Generic;
using System.Text;

namespace PasswordEncryptor.Application.RRModel.Credential
{
    public class CredentialRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class CredentialResponse
    {
        public Guid Id { get;set;  }
        public string Title { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string EncryptedPassword { get; set; } = string.Empty;
        public DateTimeOffset CreatedAt {  get; set; } 
    }

    public class CredentialUpdateRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string? Password { get; set; } 
    }
}
