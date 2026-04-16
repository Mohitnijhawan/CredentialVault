using System;
using System.Collections.Generic;
using System.Text;

namespace PasswordEncryptor.Domain.Entities
{
    public class User:BaseEntity
    {
        public string Email { get; set; } = string.Empty;

        public string ContactNo { get; set; } = string.Empty;

        public string PasswordHash { get; set; }= string.Empty;

        public string Salt {  get; set; } = string.Empty;

        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiry { get; set; }
        public int? ConfirmationCode {  get; set; }

    }
}
