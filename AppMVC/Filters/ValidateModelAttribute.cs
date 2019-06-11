using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppMVC.Filters
{
    public class ValidateModelAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
                context.Result = new BadRequestObjectResult(
                    new
                    {
                        Errors =
                        context.ModelState.Values
                        .SelectMany(e => e.Errors)
                        .Select(e => e.ErrorMessage)
                    });
        }
    }
}