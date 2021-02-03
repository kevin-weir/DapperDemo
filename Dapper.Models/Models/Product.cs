using System;

namespace Dapper.Models
{
    class Product
    {
        public long ProductId { get; set; }

        public string SKU { get; set; }

        public string ProductName { get; set; }

        public string ProductDescription { get; set; }

        public decimal UnitCost { get; set; }

        public decimal UnitPrice { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public DateTime ModifiedDateTime { get; set; }
    }
}
