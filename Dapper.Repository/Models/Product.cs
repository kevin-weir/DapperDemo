using System;

namespace Dapper.Repository.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        public string SKU { get; set; }

        public string ProductName { get; set; }

        public string ProductDescription { get; set; }

        public decimal UnitCost { get; set; }

        public decimal UnitPrice { get; set; }

        public bool IsActive { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime ModifiedDateTime { get; set; }
    }
}
