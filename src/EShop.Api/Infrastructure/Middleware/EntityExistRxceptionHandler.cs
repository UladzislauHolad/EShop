using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.Services.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace EShop.Api.Infrastructure.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class EntityExistRxceptionHandler
    {
        private readonly RequestDelegate _next;

        public EntityExistRxceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            try
            {
                await _next(context);
            }
            catch (EntityNotExistException ex)
            {
                context.Response.Clear();
                context.Response.StatusCode = 404;

                await context.Response.WriteAsync(ex.Message);

                return;
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseEntityExistExceptionHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<EntityExistRxceptionHandler>();
        }
    }
}
