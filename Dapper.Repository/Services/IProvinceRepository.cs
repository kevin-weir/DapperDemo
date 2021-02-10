using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper.Repository.Models;

namespace Dapper.Repository.Services
{
    public interface IProvinceRespository
    {
        Task<IEnumerable<Province>> GetAll();

        Task<Province> GetById(int provinceId);

        Task<IEnumerable<Province>> GetByCountryId(int countryId);

        Task<Province> Insert(Province province);

        Task<bool> Update(Province province);

        Task<bool> Delete(int provinceId);
    }
}
