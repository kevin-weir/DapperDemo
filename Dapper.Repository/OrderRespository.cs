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
        private readonly IDbConnection _connection;
        private readonly IDbTransaction _transaction;

        const string ordersSQL =
            @"SELECT *
              FROM Orders
              INNER JOIN Customers ON Orders.CustomerId = Customers.CustomerId";

        public OrderRespository(IDbConnection connection, IDbTransaction transaction = null)
        {
            _connection = connection;
            _transaction = transaction;
        }

        public async Task<PagedResults<Order>> GetAll(int page = 1, int pageSize = 10)
        {
            var orders = await GetOrdersPaged(
                ordersSQL,
                param: new
                {
                    Offset = (page - 1) * pageSize,
                    PageSize = pageSize
                },
                whereExpression: null,
                orderByExpression: "Orders.OrderId DESC",
                page: page,
                pageSize: pageSize);

            return orders;
        }

        public async Task<Order> GetById(int orderId)
        {
            var orders = await GetOrders(
                ordersSQL,
                param: new { OrderId = orderId },
                whereExpression: "Orders.OrderId = @OrderId",
                orderByExpression: null);

            return orders.FirstOrDefault();
        }

        public async Task<PagedResults<Order>> GetByCustomerId(int customerId, int page = 1, int pageSize = 10)
        {
            // Offset and PageSize need to be included in parameters for paging to function
            var pagedResults = await GetOrdersPaged(
                ordersSQL,
                param: new 
                    { CustomerId = customerId, 
                      Offset = (page - 1) * pageSize, 
                      PageSize = pageSize 
                    },
                whereExpression: "Orders.CustomerId = @CustomerId",
                orderByExpression: "Orders.OrderId DESC",
                page: page,
                pageSize: pageSize);

            return pagedResults;
        }

        private async Task<PagedResults<Order>> GetOrdersPaged(string sql, object param = null, string whereExpression = null, string orderByExpression = null, int? page = null, int? pageSize = null)
        {
            sql = SqlHelpers.SqlBuilder(sql, whereExpression, orderByExpression, page, pageSize);

            var pagedResults = new PagedResults<Order>();

            var multi = await _connection.QueryMultipleAsync(
                sql, 
                param: param, 
                transaction: _transaction);

            pagedResults.Items = multi.Read<Order, Customer, Order>((order, customer) =>
                {
                    order.Customer = customer;
                    return order;
                }, 
                splitOn: $"{nameof(Customer.CustomerId)}");

            pagedResults.TotalCount = multi.ReadFirst<int>();

            return pagedResults;
        }

        private async Task<IEnumerable<Order>> GetOrders(string sql, object param = null, string whereExpression = null, string orderByExpression = null)
        {
            sql = SqlHelpers.SqlBuilder(sql, whereExpression, orderByExpression);

            var orders = await _connection.QueryAsync<Order, Customer, Order>(
                sql,
                (order, customer) =>
                {
                    order.Customer = customer;
                    return order;
                },
                param: param,
                transaction: _transaction,
                splitOn: $"{nameof(Customer.CustomerId)}");

            return orders;
        }

        public async Task<Order> Insert(Order order)
        {
            var orderId = await _connection.InsertAsync<Order>(order, _transaction);

            return await GetById(orderId);
        }

        public async Task<bool> Update(Order order)
        {
            return await _connection.UpdateAsync<Order>(order, _transaction);
        }

        public async Task<bool> Delete(int orderId)
        {
            return await _connection.DeleteAsync<Order>(new Order { OrderId = orderId }, _transaction);
        }
    }
}
