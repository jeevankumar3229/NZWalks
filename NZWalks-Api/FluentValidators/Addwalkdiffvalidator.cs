using FluentValidation;
using NZWalks_Api.Models.DTOs;

namespace NZWalks_Api.FluentValidators
{
    public class Addwalkdiffvalidator : AbstractValidator<AddWalkDiffRequest>
    {
        public Addwalkdiffvalidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
