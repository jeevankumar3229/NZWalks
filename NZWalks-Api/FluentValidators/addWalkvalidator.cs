using FluentValidation;
using NZWalks_Api.Models.DTOs;

namespace NZWalks_Api.FluentValidators
{
    public class addWalkvalidator : AbstractValidator<AddNewWalk>
    {
        public addWalkvalidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
           
        }
    }
}
