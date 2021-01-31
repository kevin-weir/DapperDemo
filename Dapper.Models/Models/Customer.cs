using System;

namespace Dapper.Models
{
    public class Customer
    {
        public long CustomerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsActive { get; set; }
        
        public DateTime CreatedDateTime { get; set; }
    }
}
