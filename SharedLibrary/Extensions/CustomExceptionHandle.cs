using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using SharedLibrary.Dtos;
using SharedLibrary.Exceptions;

namespace SharedLibrary.Extensions
{
    public static class CustomExceptionHandle
    {
        public static void UseCustomException(this IApplicationBuilder app)
        {

            app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";
                    var errorFeature= context.Features.Get<IExceptionHandlerFeature>();
                    if (errorFeature != null)
                    {
                        var exception = errorFeature.Error;
                        ErrorDto error = null;
                        if (exception is CustomException)
                        {
                            error = new ErrorDto(exception.Message, true);
                        }
                        else
                        {
                            error = new ErrorDto(exception.Message, false);
                        }

                        var response = Response<NoDataDto>.Fail(error, 500);
                        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                    }
                });
            });
        }
    }
}
