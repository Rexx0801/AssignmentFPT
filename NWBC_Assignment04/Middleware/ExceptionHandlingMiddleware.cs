using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using NWBC_Assignment03.Models;
using System;
using System.Net;
using System.Threading.Tasks;


namespace NWBC_Assignment03.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var error = new ApiError
            {
                ErrorCode = context.Response.StatusCode,
                ErrorMessage = exception.Message
            };

            return context.Response.WriteAsync(JsonConvert.SerializeObject(error));
        }
    }
}