using System.IO.Pipelines;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PasswordEncryptor.Application.Utility;

namespace PasswordEncryptor.MiddleWare.ResultResponseMiddleWare
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ResultResponseMiddleware
    {
        private readonly RequestDelegate _next;

        public ResultResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var originalResponseStream = httpContext.Response.Body;
           using var memoryStream = new MemoryStream();
            httpContext.Response.Body= memoryStream;
            await _next(httpContext);
            memoryStream.Seek(0,SeekOrigin.Begin);

            var responseText =await  new StreamReader(memoryStream).ReadToEndAsync();
            if (!string.IsNullOrWhiteSpace(responseText))
            {
                if (IsJson.IsString(responseText))
                {
                    httpContext.Response.Body = originalResponseStream;
                    var responseObject = JsonSerializer.Deserialize<ResultResponse>(responseText,new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive=true,
                        PropertyNamingPolicy=JsonNamingPolicy.CamelCase,
                    });
                    if(responseObject?.Data is not null)
                    {
                        httpContext.Response.StatusCode = responseObject.StatusCode;
                    }
                    else if(responseObject?.ProblemDetails?.Status is not null)
                    {
                        httpContext.Response.StatusCode = responseObject.ProblemDetails.Status.Value;
                    }
                    await httpContext.Response.WriteAsync(responseText);
                }
            }
            else
            {
                memoryStream.Seek(0, SeekOrigin.Begin);
               await memoryStream.CopyToAsync(originalResponseStream);
            }
        }
    }

    public class ResultResponse
    {
        public object? Data { get; set; }

        public string Message { get; set; } = string.Empty;

        public int StatusCode { get; set; }

        public ProblemDetails? ProblemDetails { get; set; }

        public bool IsSuccess => ProblemDetails is null;
    }
    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ResultResponseMiddlewareExtensions
    {
        public static IApplicationBuilder UseResultResponseMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ResultResponseMiddleware>();
        }
    }
}
