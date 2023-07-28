using CustomerManagement.API.BusinessLogic.Constants;
using CustomerManagement.API.Core.Models.Address;
using CustomerManagement.API.Data;
using FluentValidation;

namespace CustomerManagement.API.Validators
{
    public class AddressValidator : AbstractValidator<CreateAddressDto>
    {
        public AddressValidator()
        {
            RuleFor(x => x.AddressLineOne).NotEmpty().WithMessage(ErrorMessages.MandatoryAddressLineOne)
                                        .MaximumLength(80).WithMessage(ErrorMessages.MaxLengthAddressLineOne);
            RuleFor(x => x.AddressLineTwo).MaximumLength(80).WithMessage(ErrorMessages.MaxLengthAddressLineTwo);
            RuleFor(x => x.PostCode).NotEmpty().WithMessage(ErrorMessages.MandatoryPostCode)
                                     .MaximumLength(10).WithMessage(ErrorMessages.MaxLengthPostCode);
            RuleFor(x => x.Town).NotEmpty().WithMessage(ErrorMessages.MandatoryTown)
                                 .MaximumLength(50).WithMessage(ErrorMessages.MaxLengthTown);
            RuleFor(x => x.County).MaximumLength(50).WithMessage(ErrorMessages.MaxLengthCounty);
        }
    }
}
