using System;
using Dapper.Contrib.Extensions;
using Dapper.Repository.Services;

namespace Dapper.Repository.Models
{
    [Table("Countries")]
    public class Country : IAuditable
    {
        [Key]
        public int CountryId { get; set; }

        public string CountryAbbreviation { get; set; }

        public string CountryName { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? CreatedDateTime { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime? ModifiedDateTime { get; set; }
    }
}
