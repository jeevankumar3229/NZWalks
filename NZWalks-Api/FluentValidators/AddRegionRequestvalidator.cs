using FluentValidation;
using NZWalks_Api.Models.DTOs;

namespace NZWalks_Api.FluentValidators
{
    public class AddRegionRequestvalidator:AbstractValidator<AddRegionRequest>
    {
        public AddRegionRequestvalidator()
        {
            RuleFor(x => x.Code).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
