using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using PasswordEncryptor.Application.Abstraction.Identity;
using PasswordEncryptor.Application.Abstraction.IRepository;
using PasswordEncryptor.Application.Abstraction.IServices;
using PasswordEncryptor.Application.Abstraction.IUnitOfWork;
using PasswordEncryptor.Application.RRModel.Credential;
using PasswordEncryptor.Application.Utility;
using PasswordEncryptor.Domain.Entities;

namespace PasswordEncryptor.Application.Services
{
    public class CredentialService(ICredentialRepository credentialRepository,IUnitOfWork unitOfWork,IHttpContextService httpContextService,IEncryptionService encryptionService,IMapper mapper) : ICredentialService
    {
        public async Task<Result<CredentialResponse>> AddCredential(CredentialRequest model)
        {
            var credential = mapper.Map<Credential>(model);
            credential.UserId = httpContextService.GetUserId();
           credential.EncryptedPassword= encryptionService.Encrypt(model.Password);
            await credentialRepository.AddAsync(credential);
            var returnVal=await unitOfWork.SaveChangesAsync();
            if(returnVal > 0)
            {
                var response = mapper.Map<CredentialResponse>(credential);
                return Result<CredentialResponse>.Success(response,"Credentials Added Successfully",200);
            }
            return Result<CredentialResponse>.Failure("Their is an error please try again later...", 400);
        }

        public async Task<Result<CredentialResponse>> DeleteCredential(Guid id)
        {
            var credential = await credentialRepository.GetByIdAsync(id);
            credential.UserId = httpContextService.GetUserId();
            if(credential is null)
            {
                return Result<CredentialResponse>.Failure("No credential found to delete", 400);
            }
            await credentialRepository.DeleteAsync(id);
            var returnVal=await unitOfWork.SaveChangesAsync();
            if(returnVal > 0)
            {
                var response=mapper.Map<CredentialResponse>(credential);
                return Result<CredentialResponse>.Success(response, "Credential Deleted Successfully", 200);
            }
            return Result<CredentialResponse>.Failure("Their is an error please try again later", 400);
        }

        public async Task<Result<CredentialResponse>> GetCredentialById(Guid id)
        {
            var credential = await credentialRepository.GetByIdAsync(id);

            if (credential is null)
            {
                return Result<CredentialResponse>.Failure("Not Found", 404);
            }
            var response = mapper.Map<CredentialResponse>(credential);

            return Result<CredentialResponse>.Success(response, "Credential Fetched", 200);
        }

        public async Task<Result<IEnumerable<CredentialResponse>>> GetCredentialByUsernameandTitle(string? username, string? title)
        {
            var credentials=await credentialRepository.GetCredentialsByUserNameAndTitle(username, title);
            if(credentials is null)
            {
                return Result<IEnumerable<CredentialResponse>>.Failure("No Results Found", 400);
            }
            return Result<IEnumerable<CredentialResponse>>.Success(credentials, "Fetched Filtered Results", 200);
        }

        public async Task<Result<IEnumerable<CredentialResponse>>> GetCredentials()
        {
            var credentials = await credentialRepository.GetAllCredentials();
            if(credentials is not null)
            {
                return Result<IEnumerable<CredentialResponse>>.Success(credentials,"Credentials Fetched Successfully",200);
            }
            return Result<IEnumerable<CredentialResponse>>.Failure("Their is an error try again later ",500);

        }

        public async Task<Result<string>> RevealPassword(Guid id)
        {
            var credential = await credentialRepository.GetByIdAsync(id);
            if(credential is null)
            {
                return Result<string>.Failure("Error in fetching password", 400);

            }
            var password = encryptionService.Decrypt(credential.EncryptedPassword);
            return Result<string>.Success(password, "Real Password fetched Successfully", 200);
        }



        public async Task<Result<CredentialResponse>> UpdateCredential(CredentialUpdateRequest model)
        {
            var credential = await credentialRepository.GetByIdAsync(model.Id);
            credential.UserId = httpContextService.GetUserId();
            mapper.Map(model, credential);
            if (!string.IsNullOrWhiteSpace(model.Password)){

            credential.EncryptedPassword = encryptionService.Encrypt(model.Password);
            }

            await credentialRepository.UpdateAsync(credential);
            var returnVal= await unitOfWork.SaveChangesAsync();
            if(returnVal > 0)
            {
                var response = mapper.Map<CredentialResponse>(credential);
                return Result<CredentialResponse>.Success(response, "Credentials Updated Successfully", 200);
            }
            return Result<CredentialResponse>.Failure("Their is an error please try again later...", 400);


        }
    }
}
