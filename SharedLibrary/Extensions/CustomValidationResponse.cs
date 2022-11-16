using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SharedLibrary.Dtos;

namespace SharedLibrary.Extensions
{
    public static class CustomValidationResponse
    {
        public static void UseCustomValidationResponse(this IServiceCollection services)
        {

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState.Values.Where(c => c.Errors.Count > 0).SelectMany(c => c.Errors)
                        .Select(c => c.ErrorMessage).ToList();

                    var errorDto = new ErrorDto(errors, true);

                    var response = Response<NoContentResult>.Fail(errorDto, 404);
                    
                    return new BadRequestObjectResult(response);
                };
            });
        }
    }
}
