using System;
using System.Collections.Generic;
using System.Text;

namespace PasswordEncryptor.Application.Utility
{
    public static class IsJson
    {
        public static bool IsString(string text)
        {
            if(string.IsNullOrWhiteSpace(text)) return false;
            text=text.Trim();
            return ((text.StartsWith("{") && text.EndsWith("}")) || text.StartsWith("[") &&  text.EndsWith("]"));
        }
    }
}
