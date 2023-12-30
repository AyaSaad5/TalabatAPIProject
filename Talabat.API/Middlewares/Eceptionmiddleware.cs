using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Http.Logging;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.API.Errors;

namespace Talabat.API.Middlewares
{
    public class Eceptionmiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<Eceptionmiddleware> logger;
        private readonly IHostEnvironment env;

        public Eceptionmiddleware(RequestDelegate next, ILogger<Eceptionmiddleware> logger, IHostEnvironment env)
        {
            this.next = next;
            this.logger = logger;
            this.env = env;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next.Invoke(context); // Move to Next Middleware
            }
            catch (Exception ex)
            {

                logger.LogError(ex, ex.Message);
                //Log Exception in DB

                //header of response
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

                var exceptionErrorReponse = env.IsDevelopment() ?
                    new ApiExceptionResponse(500, ex.Message, ex.StackTrace.ToString())
                    :
                    new ApiExceptionResponse(500);

                var options = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                //body of response
                var json = JsonSerializer.Serialize(exceptionErrorReponse, options);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
