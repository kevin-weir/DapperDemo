using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper.Domain.Models;
using Dapper.Repository.Models;

namespace Dapper.Repository.Interfaces
{
    public interface ICountryRespository
    {
        Task<IEnumerable<CountryDtoQuery>> GetAll();

        Task<CountryDtoQuery> GetById(int countryId);

        Task<CountryDtoQuery> Insert(Country country);

        Task<bool> Update(Country country);

        Task<bool> Delete(int countryId);

        Task<Country> GetEntityById(int countryId);
    }
}
