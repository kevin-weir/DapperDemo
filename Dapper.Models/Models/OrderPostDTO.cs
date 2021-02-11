using System;

namespace Dapper.Domain.Models
{
    public class OrderPostDTO
    {
        public int CustomerId { get; set; }

        public string OrderNumber { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal OrderTotal { get; set; }

        public decimal OrderTaxes { get; set; }
    }
}
