using System;

namespace Dapper.Models
{
    class Province
    {
        public long ProvinceId { get; set; }

        public long CountryId { get; set; }

        public string ProvinceAbbreviation { get; set; }

        public string ProvinceName { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public DateTime ModifiedDateTime { get; set; }
    }
}
