using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System;
namespace NZWalks_Api.CustomActionFilters
{
   // In the last lecture we saw how we could use Model State is valid property to check for the validation of our models, but there is a lot cleaner way which is a slightly
   // advanced technique is to create a custom action filter so we don't have to check this again and again. And the code inside the controller also is kept to a minimum.So
   // let's see how we can achieve that.For this we have to create a folder and a custom action filter.So let's right click on the folder and add a new folder and call this
   // "custom action filters".Inside this, we will create a new class.So add a new class.And you can call this ""validate model attribute"" because we are creating an
   // attribute which we can decorate the method with and that will automatically check before coming inside the method.It will check for the model.And if it is a if the
   // request is bad or it is not valid, then it will return a bad request.Otherwise it will come inside the method.So now we have to use the the "action filter attribute
   // "and inherit from that class.And now we have to override the."""On action executing""" method.So this is the ""on action executing"" method and we are overriding that
   // because we are creating a custom validate model attribute.in create opr update method, But now add the attribute to the method.So we will use the "validate model attribute" that we just created.


    public class ValidateModelsAttribute:ActionFilterAttribute //class created must have Attribute word because it is used as Attribute in controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if(context.ModelState.IsValid == false)
            {
                context.Result = new BadRequestResult();
            }
        }
    }
}
