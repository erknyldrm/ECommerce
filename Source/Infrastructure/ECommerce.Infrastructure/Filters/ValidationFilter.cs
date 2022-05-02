using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ECommerce.Infrastructure.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Where(p => p.Value.Errors.Any())
                     .ToDictionary(x => x.Key, x => x.Value.Errors.Select(k => k.ErrorMessage))
                     .ToArray();

                context.Result = new BadRequestObjectResult(errors);
                return;
            }

            await next();
        }
    }
}
