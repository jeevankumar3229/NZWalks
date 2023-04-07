using FluentValidation;
using NZWalks_Api.Models.DTOs;

namespace NZWalks_Api.FluentValidators
{
    public class updatewalkdiffvalidator : AbstractValidator<UpdateWalkDiffRequest>
    {
        public updatewalkdiffvalidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
