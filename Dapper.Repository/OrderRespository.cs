using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using Dapper.Repository.Models;
using Dapper.Repository.Services;
using Dapper.Domain.Models;
using Dapper.Repository.Helpers;

namespace Dapper.Repository
{
    public class OrderRespository : IOrderRespository
    {
        private readonly IDbConnection connection;
        private readonly IDbTransaction transaction;

        const string orderSQL =
            @"SELECT *
              FROM Order
              INNER JOIN Customer ON Order.CustomerId = Customer.CustomerId";

        public OrderRespository(IDbConnection connection, IDbTransaction transaction = null)
        {
            this.connection = connection;
            this.transaction = transaction;
        }

        //public async Task<PagedResults<OrderDtoQuery>> GetAll(int page = 1, int pageSize = 10)
        //{
        //    var orders = await GetOrders(
        //        orderSQL,
        //        param: null,
        //        whereExpression: null,
        //        orderByExpression: "Order.OrderId DESC",
        //        page: page,
        //        pageSize: pageSize);

        //    return orders;
        //}

        public async Task<PagedResults<OrderDtoQuery>> GetByCustomerId(int customerId, int page= 1, int pageSize = 10)
        {
            var orders = await GetOrders(
                orderSQL,
                param: new { CustomerId = customerId },
                whereExpression: "Order.CustomerId = @CustomerId",
                orderByExpression: "Order.OrderId DESC",
                page: page,
                pageSize: pageSize);

            return orders;
        }

        //public async Task<OrderDtoQuery> GetById(int orderId)
        //{
        //    var orders = await GetOrders(
        //        orderSQL,
        //        param: new { OrderId = orderId },
        //        whereExpression: "Order.OrderId = @OrderId",
        //        orderByExpression: null,
        //        page: null,
        //        pageSize: null);

        //    return orders.FirstOrDefault(); 
        //}

        private async Task<PagedResults<OrderDtoQuery>> GetOrders(string sql, object param = null, string whereExpression = null, string orderByExpression = null, int? page = null, int? pageSize = null)
        {
            sql = SqlHelpers.SqlBuilder(sql, whereExpression, orderByExpression, page, pageSize);



            //var customers = await connection.QueryAsync<CustomerDtoQuery, CountryDtoQuery, ProvinceDtoQuery, CustomerDtoQuery>(
            //            sql,
            //            (customer, country, province) =>
            //            {
            //                customer.Country = country;
            //                customer.Province = province;

            //                return customer;
            //            },
            //            param: param,
            //            transaction: transaction,
            //            splitOn: $"{nameof(CountryDtoQuery.CountryId)}, {nameof(ProvinceDtoQuery.ProvinceId)}");

            //return customers;

            return null;
        }

        //public async Task<OrderDtoQuery> Insert(Order order)
        //{
        //    var orderId = await connection.InsertAsync<Order>(order, transaction);

        //    return await GetById(orderId);
        //}

        public async Task<bool> Update(Order order)
        {
            return await connection.UpdateAsync<Order>(order, transaction);
        }

        public async Task<bool> Delete(int orderId)
        {
            return await connection.DeleteAsync<Order>(new Order { OrderId = orderId }, transaction);
        }
        
        public async Task<Order> GetEntityById(int orderId)
        {
            return await connection.GetAsync<Order>(orderId, transaction);
        }
    }
}
