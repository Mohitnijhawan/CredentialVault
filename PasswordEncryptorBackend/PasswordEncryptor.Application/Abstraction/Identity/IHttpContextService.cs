using System;
using System.Collections.Generic;
using System.Text;

namespace PasswordEncryptor.Application.Abstraction.Identity
{
    public interface IHttpContextService
    {
        Guid GetUserId();

        string GetCurrentClientUrl();

        string GetCurrentUrl();
        
    }
}
