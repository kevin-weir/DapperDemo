using System;

namespace Dapper.Models
{
    class Country
    {
        public long CountryId { get; set; }

        public string CountryAbbreviation { get; set; }

        public string CountryName { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public DateTime ModifiedDateTime { get; set; }
    }
}
