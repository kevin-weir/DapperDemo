using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper.Models;
using Dapper.Contrib.Extensions;

namespace Dapper.Repository
{
    public class CustomerRespository : ICustomerRespository
    {
        private readonly IDbConnection connection;
        private readonly IDbTransaction transaction;

        public CustomerRespository(IDbConnection connection, IDbTransaction transaction = null)
        {
            this.connection = connection;
            this.transaction = transaction;
        }

        public async Task<Customer> GetById(long customerId)
        {
            return await connection.GetAsync<Customer>(customerId, transaction);
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await connection.GetAllAsync<Customer>(transaction);
        }

        public async Task<Customer> Insert(Customer customer)
        {
            customer.CustomerId = await connection.InsertAsync<Customer>(customer, transaction);
            return customer;
        }

        public async Task<bool> Update(Customer customer)
        {
            return await connection.UpdateAsync<Customer>(customer, transaction);
        }

        public async Task<bool> Delete(long customerId)
        {
            return await connection.DeleteAsync<Customer>(new Customer { CustomerId = customerId }, transaction);
        }
    }
}
