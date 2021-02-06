using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using Dapper.Repository.Models;
using Dapper.Repository.Interfaces;
using Dapper.Domain.Models;

namespace Dapper.Repository
{
    public class CustomerRespository : ICustomerRespository
    {
        private readonly IDbConnection connection;
        private readonly IDbTransaction transaction;

        const string customerSQL =
            @"SELECT *
             FROM Customer
             LEFT OUTER JOIN Country ON Customer.CountryId = Country.CountryId
             LEFT OUTER JOIN Province ON Customer.ProvinceId = Province.ProvinceId";

        public CustomerRespository(IDbConnection connection, IDbTransaction transaction = null)
        {
            this.connection = connection;
            this.transaction = transaction;
        }

        public async Task<CustomerDtoQuery> GetById(int customerId)
        {
            var sql = customerSQL + " WHERE Customer.CustomerId = @CustomerId";

            return (await GetCustomers(sql, param: new { CustomerId = customerId })).FirstOrDefault(); 
        }

        public async Task<IEnumerable<CustomerDtoQuery>> GetAll()
        {
             var sql = customerSQL;

             return await GetCustomers(sql, param: null);
        }

        private async Task<IEnumerable<CustomerDtoQuery>> GetCustomers(string sql, object param = null)
        {
            var customers = await connection.QueryAsync<CustomerDtoQuery, CountryDtoQuery, ProvinceDtoQuery, CustomerDtoQuery>(
                        sql,
                        (customer, country, province) =>
                        {
                            customer.Country = country;
                            customer.Province = province;
                            return customer;
                        },
                        param: param,
                        splitOn: $"{nameof(CountryDtoQuery.CountryId)}, {nameof(ProvinceDtoQuery.ProvinceId)}");

            return customers;
        }

        public async Task<CustomerDtoQuery> Insert(Customer customer)
        {
            var customerId = await connection.InsertAsync<Customer>(customer, transaction);

            return await GetById(customerId);
        }

        public async Task<bool> Update(Customer customer)
        {
            return await connection.UpdateAsync<Customer>(customer, transaction);
        }

        public async Task<bool> Delete(int customerId)
        {
            return await connection.DeleteAsync<Customer>(new Customer { CustomerId = customerId }, transaction);
        }
        
        public async Task<Customer> GetEntityById(int customerId)
        {
            return await connection.GetAsync<Customer>(customerId);
        }
    }
}
