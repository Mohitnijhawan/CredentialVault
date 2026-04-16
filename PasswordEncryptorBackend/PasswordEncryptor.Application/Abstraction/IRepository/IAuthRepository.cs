using System;
using System.Collections.Generic;
using System.Text;
using PasswordEncryptor.Domain.Entities;

namespace PasswordEncryptor.Application.Abstraction.IRepository
{
    public interface IAuthRepository:IBaseRepository<User>
    {
    }
}
