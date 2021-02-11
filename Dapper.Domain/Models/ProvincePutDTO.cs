namespace Dapper.Domain.Models
{
    public class ProvincePutDTO
    {
        public int ProvinceId { get; set; }

        public int CountryId { get; set; }

        public string ProvinceAbbreviation { get; set; }

        public string ProvinceName { get; set; }
    }
}
