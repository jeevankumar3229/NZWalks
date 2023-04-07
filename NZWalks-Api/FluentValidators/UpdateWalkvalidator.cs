using FluentValidation;
using NZWalks_Api.Models.DTOs;

namespace NZWalks_Api.FluentValidators
{
    public class UpdateWalkvalidator : AbstractValidator<UpdateWalk>
    {
        public UpdateWalkvalidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            
        }
    }
}