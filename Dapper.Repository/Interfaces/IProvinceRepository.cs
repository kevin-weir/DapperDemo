using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper.Domain.Models;
using Dapper.Repository.Models;

namespace Dapper.Repository.Interfaces
{
    public interface IProvinceRespository
    {
        Task<IEnumerable<ProvinceDtoQuery>> GetAll();

        Task<ProvinceDtoQuery> GetById(int provinceId);

        Task<IEnumerable<ProvinceDtoQuery>> GetByCountryId(int countryId);

        Task<ProvinceDtoQuery> Insert(Province province);

        Task<bool> Update(Province province);

        Task<bool> Delete(int provinceId);

        Task<Province> GetEntityById(int provinceId);
    }
}
