using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper.Models;

namespace Dapper.Repository
{
    public interface ICustomerRespository
    {
        Task<IEnumerable<Customer>> GetAll();

        Task<Customer> GetById(long customerId);

        Task<Customer> Insert(Customer customer);

        Task<bool> Update(Customer customer);

        Task<bool> Delete(long customerId);
    }
}
