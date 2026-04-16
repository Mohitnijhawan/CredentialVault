using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace PasswordEncryptor.Application.RRModel.Auth
{
    public class RefreshTokenResponse
    {
        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; }

        [JsonPropertyName("refreshToken")]
        public string RefreshToken { get; set; }
    }
}

