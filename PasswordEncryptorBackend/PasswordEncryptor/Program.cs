using PasswordEncryptor;
using PasswordEncryptor.MiddleWare.CustomMiddleWare;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddApiService(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCustomMiddleWare();


