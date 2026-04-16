using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using PasswordEncryptor.Application.Abstraction.AppEncryption;
using PasswordEncryptor.Application.Abstraction.Identity;
using PasswordEncryptor.Application.Abstraction.IJWTProvider;
using PasswordEncryptor.Application.Abstraction.IRepository;
using PasswordEncryptor.Application.Abstraction.IServices;
using PasswordEncryptor.Application.Abstraction.IUnitOfWork;
using PasswordEncryptor.Application.RRModel.Auth;
using PasswordEncryptor.Application.Utility;
using PasswordEncryptor.Domain.Entities;

namespace PasswordEncryptor.Application.Services
{
    public class AuthService(IAuthRepository authRepository, IMapper mapper, IUnitOfWork unitOfWork, IAppEncryption appEncryption, IJWTProvider jWTProvider, IHttpContextService httpContextService) : IAuthService
    {

        public async Task<Result<LoginResponse>> Login(LoginRequest model)
        {
            var user = await authRepository.FirstOrDefaultAsync(x => x.Email == model.Email);
            if (user is null)
            {
                return Result<LoginResponse>.Failure("Username or Password is Incorrect", 400);
            }
            var hashPassword = appEncryption.HashPassword(model.Password, user.Salt);
            if (!hashPassword.Equals(user.PasswordHash))
            {
                return Result<LoginResponse>.Failure("Username or Password is Incorrect", 400);
            }

            var accessToken = jWTProvider.GenerateAuthToken(user);
            var refreshToken = jWTProvider.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);

            await unitOfWork.SaveChangesAsync();

            var loginResponse = new LoginResponse
            {
                UserId = user.Id,
                Email = user.Email,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
            };
            return Result<LoginResponse>.Success(loginResponse, "Logged in Successfully", 200);
        }
        

        public async Task<Result<string>> LogOut()
        {
            var userId = httpContextService.GetUserId();
            var user = await authRepository.GetByIdAsync(userId);
            if (user is null)
            {
                return Result<string>.Failure("No user found", 500);
            }
            if (user != null)
            {
                user.RefreshToken = null;
                user.RefreshTokenExpiry = null;

                await unitOfWork.SaveChangesAsync();
            }
            return Result<string>.Success("Logout successfully");
        }

        public async Task<Result<RefreshTokenResponse>> RefreshToken(RefreshTokenRequest model)
        {
            var user = await authRepository
         .FirstOrDefaultAsync(x => x.RefreshToken == model.RefreshToken);

            if (user == null)
                return Result<RefreshTokenResponse>.Failure("Invalid refresh token", 401);

            if (user.RefreshTokenExpiry < DateTime.UtcNow)
                return Result<RefreshTokenResponse>.Failure("Refresh token expired", 401);

            var newAccessToken = jWTProvider.GenerateAuthToken(user);
            var newRefreshToken = jWTProvider.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);

            await unitOfWork.SaveChangesAsync();

            return Result<RefreshTokenResponse>.Success(new RefreshTokenResponse
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            }, "Token refreshed", 200);

        }

        public  async Task<Result<SignUpResponse>> SignUp(SignUpRequest model)
        {
            var isExists = await authRepository.FirstOrDefaultAsync(x => x.Email == model.Email);
            if (isExists is null)
            {
                var user = mapper.Map<User>(model);
                var salt = appEncryption.GenerateSalt();
                var PasswordHash = appEncryption.HashPassword(model.Password, salt);
                user.Salt = salt;
                user.PasswordHash = PasswordHash;
                await authRepository.AddAsync(user);
                var returnVal = await unitOfWork.SaveChangesAsync();
                if (returnVal > 0)
                {
                    var response = mapper.Map<SignUpResponse>(user);
                    return Result<SignUpResponse>.Success(response, "Signed up Successfully", 200);
                }
                return Result<SignUpResponse>.Failure("Their is an error please try again later...", 500);
            }
            return Result<SignUpResponse>.Failure("Email Already Exists", 400);
        }
    }
}
