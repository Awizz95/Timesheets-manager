using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using TimesheetsProj.Infrastructure.Validation;

namespace TimesheetsProj.Controllers
{
    public class TimesheetBaseController : Controller
    {
        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!ModelState.IsValid)
            {
                ErrorModel result = new ErrorModel { Errors = new Dictionary<string, string>() };

                foreach (ModelStateEntry value in context.ModelState.Values)
                {
                    if (value.ValidationState == ModelValidationState.Invalid)
                    {
                        string? propertyValue = value.GetType().GetProperties()
                            .FirstOrDefault(s => s.Name == "Key")?
                            .GetValue(value, null)?.ToString().ToLower();

                        if (propertyValue != null)
                        {
                            if (!result.Errors.ContainsKey(propertyValue))
                                result.Errors.Add(propertyValue, value.Errors.FirstOrDefault()?.ErrorMessage
                                                                 ?? "Invalid Value");
                        }
                    }
                }

                context.Result = new BadRequestObjectResult(result);
                return Task.CompletedTask;
            }

            return base.OnActionExecutionAsync(context, next);
        }
    }
}
