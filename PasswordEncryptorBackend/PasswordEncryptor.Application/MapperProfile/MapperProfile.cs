using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using PasswordEncryptor.Application.RRModel.Auth;
using PasswordEncryptor.Application.RRModel.Credential;
using PasswordEncryptor.Domain.Entities;

namespace PasswordEncryptor.Application.MapperProfile
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<SignUpRequest, User>().ReverseMap();
            CreateMap<SignUpResponse, User>().ReverseMap();
        }
    }

    public class CredentialProfile : Profile
    {
        public CredentialProfile()
        {
            CreateMap<CredentialRequest, Credential>().ReverseMap();
            CreateMap<Credential, CredentialResponse>().ReverseMap();
            CreateMap<CredentialUpdateRequest, Credential>().ReverseMap();
        }
    }
}
