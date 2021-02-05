﻿namespace Dapper.Domain.Models
{
    public class CustomerDtoUpdate
    {
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
    }
}
