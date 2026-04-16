using System;
using System.Collections.Generic;
using System.Text;

namespace PasswordEncryptor.Domain.Entities
{
    public class ErrorLog:BaseEntity
    {
        public string Message { get; set; } = string.Empty;

        public string StackTrace { get; set; } = string.Empty;

        public string Source { get; set; } = string.Empty;
    }
}
