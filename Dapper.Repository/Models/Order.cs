using System;
using Dapper.Contrib.Extensions;
using Dapper.Repository.Services;

namespace Dapper.Repository.Models
{
    [Table("Orders")]
    public class Order : IAuditable
    {
        [Key]
        public int OrderId { get; set; }

        public int CustomerId { get; set; }

        [Write(false)]
        public Customer Customer { get; set; }

        public string OrderNumber { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal OrderTotal { get; set; }

        public decimal OrderTaxes { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? CreatedDateTime { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime? ModifiedDateTime { get; set; }
    }
}
