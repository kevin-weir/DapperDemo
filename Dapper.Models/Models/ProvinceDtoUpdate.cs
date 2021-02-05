namespace Dapper.Domain.Models
{
    public class ProvinceDtoUpdate
    {
        public int ProvinceId { get; set; }

        public int CountryId { get; set; }

        public string ProvinceAbbreviation { get; set; }

        public string ProvinceName { get; set; }
    }
}
