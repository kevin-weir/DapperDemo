using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using Dapper.Repository.Models;
using Dapper.Repository.Services;
using Dapper.Repository.Helpers;

namespace Dapper.Repository
{
    public class CustomerRespository : ICustomerRespository
    {
        private readonly IDbConnection connection;
        private readonly IDbTransaction transaction;

        const string customersSQL =
            @"SELECT *
              FROM Customers
              LEFT OUTER JOIN Countries ON Customers.CountryId = Countries.CountryId
              LEFT OUTER JOIN Provinces ON Customers.ProvinceId = Provinces.ProvinceId";

        public CustomerRespository(IDbConnection connection, IDbTransaction transaction = null)
        {
            this.connection = connection;
            this.transaction = transaction;
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            var customers = await GetCustomers(
                customersSQL,
                param: null,
                whereExpression: null,
                orderByExpression: "Customers.LastName ASC");

            return customers;
        }

        public async Task<Customer> GetById(int customerId)
        {
            var customers = await GetCustomers(
                customersSQL,
                param: new { CustomerId = customerId },
                whereExpression: "Customers.CustomerId = @CustomerId",
                orderByExpression: null);

            return customers.FirstOrDefault(); 
        }

        private async Task<IEnumerable<Customer>> GetCustomers(string sql, object param = null, string whereExpression = null, string orderByExpression = null)
        {
            sql = SqlHelpers.SqlBuilder(sql, whereExpression, orderByExpression);

            var customers = await connection.QueryAsync<Customer, Country, Province, Customer>(
                        sql,
                        (customer, country, province) =>
                        {
                            customer.Country = country;
                            customer.Province = province;

                            return customer;
                        },
                        param: param,
                        transaction: transaction,
                        splitOn: $"{nameof(Country.CountryId)}, {nameof(Province.ProvinceId)}");

            return customers;
        }

        public async Task<Customer> Insert(Customer customer)
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
    }
}
