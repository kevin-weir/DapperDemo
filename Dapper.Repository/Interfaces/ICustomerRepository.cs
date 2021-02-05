using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper.Domain.Models;
using Dapper.Repository.Models;

namespace Dapper.Repository.Interfaces
{
    public interface ICustomerRespository
    {
        Task<IEnumerable<CustomerDtoQuery>> GetAll();

        Task<CustomerDtoQuery> GetById(int customerId);

        Task<CustomerDtoQuery> Insert(Customer customer);

        Task<bool> Update(Customer customer);

        Task<bool> Delete(int customerId);
    }
}
