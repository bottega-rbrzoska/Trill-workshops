using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Trill.Core.Exceptions;

namespace Trill.Infrastructure.Middlewares
{
    internal class ErrorHandlerMiddleware : IMiddleware
    {
        private static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Formatting = Formatting.Indented
        };
    
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                switch (exception)
                {
                    case DomainException domainException:
                        await HandleCustomException(context, domainException.Code, domainException.Message);
                        return;
                    default:
                        throw;
                }
            }
        }

        private static async Task HandleCustomException(HttpContext context, string code, string message)
        {
            context.Response.StatusCode = 400;
            var response = new
            {
                code, message
            };
            await context.Response.WriteAsync(JsonConvert.SerializeObject(response, JsonSettings));
        }
    }
}