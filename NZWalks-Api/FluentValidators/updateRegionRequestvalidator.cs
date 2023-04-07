using FluentValidation;
using NZWalks_Api.Models.DTOs;

namespace NZWalks_Api.FluentValidators
{
    public class updateRegionRequestvalidator : AbstractValidator<UpdateRegionRequest>
    {
        public updateRegionRequestvalidator()
        {

            RuleFor(x => x.Code).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
