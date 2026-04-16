using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PasswordEncryptor.Application.Utility
{
    public class Result<T>
    {
        public T? Data {  get; set; }

        public string Message { get; set; } = string.Empty;

        public int StatusCode { get; set; }

        public ProblemDetails ProblemDetails { get; set; }

        public bool IsSuccess => ProblemDetails is null;

        public Result(T?data=default,int statusCode=StatusCodes.Status200OK)
        {
            Data=data;
            StatusCode=statusCode;
        }

        public Result(ProblemDetails problemDetails)
        {
            ProblemDetails=problemDetails;
        }

        public static Result<T> Success(T? data)
        {
            return new Result<T>
            {
                Data = data,
            };
        }

        public static Result<T> Success(T? data,string message,int statusCode)
        {
            return new Result<T>
            {
                Data = data,
                Message = message,
                StatusCode = statusCode
            };
        }

        public static Result<T> Success(T? data,int statusCode)
        {
            return new Result<T>
            {
                Data = data,
                StatusCode = statusCode
            };
        }

        public static Result<T> Failure(ProblemDetails problemDetails)
        {
            var details = new ProblemDetails
            {
                Type = problemDetails.Type,
                Detail=problemDetails.Detail,
                Instance=problemDetails.Instance,
                Status=problemDetails.Status,
                Title=problemDetails.Title,
            };
            return new Result<T>(details);
        }

        public static Result<T> Failure(string message,int statusCode=StatusCodes.Status400BadRequest)
        {
            var details = new ProblemDetails
            {
                Status = statusCode,
                Title = message,
            };
            return new Result<T>(details);
        }

    }
}
