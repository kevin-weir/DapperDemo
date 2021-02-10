using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper.Repository.Models;

namespace Dapper.Repository.Services
{
    public interface ICustomerRespository
    {
        Task<IEnumerable<Customer>> GetAll();

        Task<Customer> GetById(int customerId);

        Task<Customer> Insert(Customer customer);

        Task<bool> Update(Customer customer);

        Task<bool> Delete(int customerId);
    }
}
