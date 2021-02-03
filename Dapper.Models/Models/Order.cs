using System;

namespace Dapper.Models
{
    class Order
    {
        public long OrderId { get; set; }

        public long CustomerId { get; set; }

        public string OrderNumber { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal OrderTotal { get; set; }

        public decimal OrderTaxes { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public DateTime ModifiedDateTime { get; set; }
    }
}
