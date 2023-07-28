using CustomerManagement.API.BusinessLogic.Constants;
using CustomerManagement.API.Core.Models.Customer;
using FluentValidation;

namespace CustomerManagement.API.Validators
{
    public class CustomerValidator : AbstractValidator<CreateCustomerDto>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage(ErrorMessages.MandatoryTitle)
                                        .MaximumLength(20).WithMessage(ErrorMessages.MaxLengthTitle);
            RuleFor(x => x.Forename).NotEmpty().WithMessage(ErrorMessages.MandatoryForename)
                .MaximumLength(50).WithMessage(ErrorMessages.MaxLengthForename);
            RuleFor(x => x.Surname).NotEmpty().WithMessage(ErrorMessages.MandatorySurname)
                                     .MaximumLength(50).WithMessage(ErrorMessages.MaxLengthSurname);
            RuleFor(x => x.EmailAddress).EmailAddress().WithMessage(ErrorMessages.InvalidEmailAddress)
                                 .MaximumLength(75).WithMessage(ErrorMessages.MaxLengthEmailAddress);
            RuleFor(x => x.MobileNumber).NotEmpty().WithMessage(ErrorMessages.MandatoryMobileNumber)
                .MaximumLength(15).WithMessage(ErrorMessages.MaxLengthMobileNumber);
        }
    }
}
