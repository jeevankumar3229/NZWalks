using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace NZAPI.CustomActionFilters
{
    public class ValidateModelsAttribute : ActionFilterAttribute //class created must have Attribute word because it is used as Attribute in controller
    {
            public override void OnActionExecuting(ActionExecutingContext context)
            {
                if (context.ModelState.IsValid == false)
                {
                    context.Result = new BadRequestResult();
                }
            }
        
    }
}
