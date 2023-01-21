using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using API.Model;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace API.Middlewares;

public sealed class ApiExceptionHandlingMiddleware
{
    private static readonly IList<Map> Mapping = new List<Map>
    {
        new(typeof(ArgumentNullException), HttpStatusCode.BadRequest, "Required argument is not supplied"),
        new(typeof(ArgumentException), HttpStatusCode.BadRequest, "Invalid argument supplied"),
        new(typeof(EntityNotFoundException), HttpStatusCode.NotFound, "Specified entity does not exist"),
        new(typeof(InvalidOperationException), HttpStatusCode.InternalServerError, "Server logical error"),
        new(typeof(Exception), HttpStatusCode.InternalServerError, "Unknown error")
    };

    private readonly IActionResultExecutor<ObjectResult> _actionResultExecutor;

    private readonly RequestDelegate _next;

    public ApiExceptionHandlingMiddleware(RequestDelegate next,
        IActionResultExecutor<ObjectResult> actionResultExecutor)
    {
        _next = next;
        _actionResultExecutor = actionResultExecutor;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        async Task ReturnErrorResponseAsync(HttpStatusCode statusCode, object result)
        {
            context.Response.StatusCode = (int) statusCode;

            await _actionResultExecutor.ExecuteAsync(new ActionContext { HttpContext = context },
                new ObjectResult(result)).ConfigureAwait(false);
        }

        try
        {
            await _next(context).ConfigureAwait(true);
        }
        catch (Exception exception)
        {
            var map = Mapping.First(m => m.ExceptionType.IsInstanceOfType(exception));

            await ReturnErrorResponseAsync(map.StatusCode, new ErrorDto(map.ErrorMessage, exception.Message))
                .ConfigureAwait(true);
        }
    }

    private class Map
    {
        public Map(Type exceptionType, HttpStatusCode statusCode, string errorMessage)
        {
            ExceptionType = exceptionType;
            StatusCode = statusCode;
            ErrorMessage = errorMessage;
        }

        public Type ExceptionType { get; }
        public HttpStatusCode StatusCode { get; }
        public string ErrorMessage { get; }
    }
}