﻿using System;

namespace Dapper.Domain.Models
{
    public class ProvinceResponseDTO
    {
        public int ProvinceId { get; set; }

        public int CountryId { get; set; }

        public CountryResponseDTO Country { get; set; }

        public string ProvinceAbbreviation { get; set; }

        public string ProvinceName { get; set; }

        public DateTime? CreatedDateTime { get; set; }

        public DateTime? ModifiedDateTime { get; set; }
    }
}
