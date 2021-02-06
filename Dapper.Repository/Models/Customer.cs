using System;
using Dapper.Repository.Interfaces;
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

        /// <summary>
        /// The country where the customer resides
        /// </summary>
        /// <example>null</example> 
        public int? CountryId { get; set; }

        /// <summary>
        /// The province where the customer resides
        /// </summary>
        /// <example>null</example> 
        public int? ProvinceId { get; set; }

        public string PostalCode { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? CreatedDateTime { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime? ModifiedDateTime { get; set; }
    }
}
