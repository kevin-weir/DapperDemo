﻿using System;

namespace Dapper.Domain.Models
{
    public class OrderResponseDTO
    {
        public int OrderId { get; set; }

        public int CustomerId { get; set; }

        public CustomerResponseDTO Customer { get; set; }

        public string OrderNumber { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal OrderTotal { get; set; }

        public decimal OrderTaxes { get; set; }

        public DateTime? CreatedDateTime { get; set; }

        public DateTime? ModifiedDateTime { get; set; }
    }
}
