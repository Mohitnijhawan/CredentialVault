using Carter;
using PasswordEncryptor.MiddleWare.ResultResponseMiddleWare;

namespace PasswordEncryptor.MiddleWare.CustomMiddleWare
{
    public static class RequestPipeLine
    {
        public static WebApplication UseCustomMiddleWare(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseCors(options =>
            {
                options.SetIsOriginAllowed(_=>true).AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader();
            });

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapCarter();

            app.UseResultResponseMiddleware();

            app.Run();

            return app;
        }
    }
}
