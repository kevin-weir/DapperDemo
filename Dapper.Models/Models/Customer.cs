using System;

namespace Dapper.Models
{
    public class Customer
    {
        public long CustomerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string StreetAddress { get; set; }

        public string City { get; set; }

        public long CountryId { get; set; }

        public long ProvinceId { get; set; }

        public string PostalCode { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public DateTime ModifiedDateTime { get; set; }
    }
}
