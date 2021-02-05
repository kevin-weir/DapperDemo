using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper.Repository.Models;
using Dapper.Domain.Models;
using Dapper.Contrib.Extensions;
using Dapper.Repository.Interfaces;

using System.Linq;

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
            //return await connection.GetAsync<Customer>(customerId, transaction);

            var sql = customerSQL + " WHERE Customer.CustomerId = @CustomerId";

            IEnumerable<CustomerDtoQuery> customers = await connection.QueryAsync<CustomerDtoQuery, CountryDtoQuery, ProvinceDtoQuery, CustomerDtoQuery>(
            sql,
            (customer, country, province) =>
            {
                customer.Country = country;
                customer.Province = province;
                return customer;
            },
            param: new { CustomerId = customerId},
            splitOn: "CountryId, ProvinceId");

            return customers.FirstOrDefault();
        }

        public async Task<IEnumerable<CustomerDtoQuery>> GetAll()
        {
            //return await connection.GetAllAsync<Customer>(transaction);



            //string sql = "SELECT * FROM Customer AS A LEFT OUTER JOIN Country AS B ON A.CountryId = B.CountryId;";

            //IEnumerable<Customer> customers = await connection.QueryAsync<Customer, Country, Customer>(
            //            sql,
            //            (customer, country) =>
            //            {
            //                customer.Country = country;
            //                return customer;
            //            },
            //            splitOn: "CountryId");
                    //.Distinct()
                    //.ToList();

            //return customers;



            //string sql = "SELECT * FROM Customer AS A LEFT OUTER JOIN Country AS B ON A.CountryId = B.CountryId LEFT OUTER JOIN Province AS C ON A.ProvinceId = C.ProvinceId;";
            //string sql = @"SELECT * 
            //               FROM Customer AS A 
            //               LEFT OUTER JOIN Country AS B ON A.CountryId = B.CountryId 
            //               LEFT OUTER JOIN Province AS C ON A.ProvinceId = C.ProvinceId;";

            IEnumerable<CustomerDtoQuery> customers = await connection.QueryAsync<CustomerDtoQuery, CountryDtoQuery, ProvinceDtoQuery, CustomerDtoQuery>(
                        customerSQL,
                        (customer, country, province) =>
                        {
                            customer.Country = country;
                            customer.Province = province;
                            return customer;
                        },
                        splitOn: "CountryId, ProvinceId");

            return customers;
        }

        public async Task<CustomerDtoQuery> Insert(Customer customer)
        {
            var customerId = await connection.InsertAsync<Customer>(customer, transaction);
            var newCustomer = await GetById(customerId);

            return newCustomer;
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
