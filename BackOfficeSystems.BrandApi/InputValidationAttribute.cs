using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using BackOfficeSystems.BrandApi.Api.Models;
using Microsoft.AspNetCore.Http;

namespace BackOfficeSystems.BrandApi
{
    public class InputValidationAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
            {
                return;
            }

            var formattedErrors = string.Join(" ",
                context.ModelState.Values.SelectMany(v => v.Errors).Select(error => error.ErrorMessage));

            var responseObj = new ErrorResponse {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = $"The input is not valid: {formattedErrors}",
            };

            context.Result = new ObjectResult(responseObj)
            {
                StatusCode = StatusCodes.Status400BadRequest
            };
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}