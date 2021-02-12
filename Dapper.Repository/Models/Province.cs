using System;
using Dapper.Contrib.Extensions;
using Dapper.Repository.Services;

namespace Dapper.Repository.Models
{
    [Table("Provinces")]
    public class Province : IAuditable
    {
        [Key]
        public int ProvinceId { get; set; }

        public int CountryId { get; set; }

        [Write(false)]
        public Country Country { get; set; }

        public string ProvinceAbbreviation { get; set; }

        public string ProvinceName { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? CreatedDateTime { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime? ModifiedDateTime { get; set; }
    }
}
