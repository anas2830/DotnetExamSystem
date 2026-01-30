
using System;

namespace DotnetExamSystem.Api.Exceptions;

public class ApiException : Exception
{
    public int StatusCode { get; set; } = 400;

    public ApiException(string message, int statusCode = 400) : base(message)
    {
        StatusCode = statusCode;
    }
}
