using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper.Repository.Models;

namespace Dapper.Repository.Services
{
    public interface ICountryRespository
    {
        Task<IEnumerable<Country>> GetAll();

        Task<Country> GetById(int countryId);

        Task<Country> Insert(Country country);

        Task<bool> Update(Country country);

        Task<bool> Delete(int countryId);
    }
}
