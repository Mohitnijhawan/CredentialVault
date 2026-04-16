using System;
using System.Collections.Generic;
using System.Text;
using PasswordEncryptor.Application.RRModel.Auth;
using PasswordEncryptor.Application.Utility;

namespace PasswordEncryptor.Application.Abstraction.IServices
{
    public interface IAuthService
    {
        Task<Result<SignUpResponse>> SignUp(SignUpRequest model);

        Task<Result<LoginResponse>> Login(LoginRequest model);

        Task<Result<RefreshTokenResponse>> RefreshToken(RefreshTokenRequest model);

        Task<Result<string>> LogOut();

    }
}
