using FluentValidation;

namespace Dapper.Domain.Models
{
    // ********** Customer Validation Start **********
    public class CustomerDtoInsertValidator : AbstractValidator<CustomerDtoInsert>
    {
        public CustomerDtoInsertValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Please enter the customers first name");
            RuleFor(x => x.FirstName).Length(0, 50).WithMessage("Please enter a first name no longer than 50 characters");
        }
    }
    public class CustomerDtoUpdateValidator : AbstractValidator<CustomerDtoUpdate>
    {
        public CustomerDtoUpdateValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Please enter the customers first name");
            RuleFor(x => x.FirstName).Length(0, 50).WithMessage("Please enter a first name no longer than 50 characters");
        }
    }
    // ********** Customer Validation End **********
}
