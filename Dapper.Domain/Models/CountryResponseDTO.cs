﻿using System;

namespace Dapper.Domain.Models
{
    public class CountryResponseDTO
    {
        public int CountryId { get; set; }

        public string CountryAbbreviation { get; set; }

        public string CountryName { get; set; }

        public DateTime? CreatedDateTime { get; set; }

        public DateTime? ModifiedDateTime { get; set; }
    }
}
