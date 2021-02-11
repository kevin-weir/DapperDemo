using FluentValidation;

namespace Dapper.Domain.Models
{
    // ********** Customer Validation Start **********
    public class CustomerPostDTOValidator : AbstractValidator<CustomerPostDTO>
    {
        public CustomerPostDTOValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Please enter the customers first name");
            RuleFor(x => x.FirstName).Length(0, 50).WithMessage("Please enter a first name no longer than 50 characters");
        }
    }
    public class CustomerPutDTOValidator : AbstractValidator<CustomerPutDTO>
    {
        public CustomerPutDTOValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Please enter the customers first name");
            RuleFor(x => x.FirstName).Length(0, 50).WithMessage("Please enter a first name no longer than 50 characters");
        }
    }
    // ********** Customer Validation End **********
}
