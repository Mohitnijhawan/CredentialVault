using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using PasswordEncryptor.Application.Abstraction.Identity;
using PasswordEncryptor.InfraStructure.JWTProvider;

namespace PasswordEncryptor.InfraStructure.Identity
{
    public class HttpContextService(IHttpContextAccessor httpContext) : IHttpContextService
    {
        public string GetCurrentClientUrl()
        {
            return httpContext.HttpContext.Request.Host.Value;
        }

        public string GetCurrentUrl()
        {
            return httpContext.HttpContext.Response.Headers["referer"]!;
        }

        public Guid GetUserId()
        {
            var strUserId = httpContext?.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == UserClaims.UserId)?.Value;
            if (Guid.TryParse(strUserId, out Guid userid)) return userid;
            return Guid.Empty;
        }
    }
}
