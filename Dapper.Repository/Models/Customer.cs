using System;
using FluentValidation;
using Dapper.Repository.Services;
using Dapper.Contrib.Extensions;

namespace Dapper.Repository.Models
{
    [Table("Customer")]
    public class Customer : IAuditable
    {
        [Key]
        public int CustomerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string StreetAddress { get; set; }

        public string City { get; set; }

        public int? CountryId { get; set; }

        [Write(false)]
        public Country Country { get; set; }

        public int? ProvinceId { get; set; }

        [Write(false)]
        public Province Province { get; set; }

        public string PostalCode { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? CreatedDateTime { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime? ModifiedDateTime { get; set; }
    }

    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Please enter the customers first name");
            RuleFor(x => x.FirstName).Length(0, 50).WithMessage("Please enter a first name no longer than 50 characters");
        }
    }
}
