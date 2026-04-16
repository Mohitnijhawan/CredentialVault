using Microsoft.AspNetCore.Diagnostics;
using PasswordEncryptor.Domain.Entities;
using PasswordEncryptor.Persistance.Data;

namespace PasswordEncryptor
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public GlobalExceptionHandler(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            using var scope = _serviceScopeFactory.CreateScope();

            var db = scope.ServiceProvider.GetRequiredService<PasswordEncryptorDbContext>();

            var error = new ErrorLog
            {
                Message = exception.Message,
                StackTrace = exception.StackTrace!,
                Source = exception.Source!,
            };

            db.ErrorLogs.Add(error);
            await db.SaveChangesAsync(cancellationToken);

            httpContext.Response.StatusCode = 500;
            await httpContext.Response.WriteAsJsonAsync(new
            {
                message = "Something went wrong"
            });

            return true;
        }
    }
}
