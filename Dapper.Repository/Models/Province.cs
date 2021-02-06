using System;
using Dapper.Contrib.Extensions;

namespace Dapper.Repository.Models
{
    [Table("Province")]
    public class Province
    {
        [Key]
        public int ProvinceId { get; set; }

        public int CountryId { get; set; }

        public string ProvinceAbbreviation { get; set; }

        public string ProvinceName { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? CreatedDateTime { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime? ModifiedDateTime { get; set; }
    }
}
