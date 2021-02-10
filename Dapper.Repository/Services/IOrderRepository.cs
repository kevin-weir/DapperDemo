using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper.Domain.Models;
using Dapper.Repository.Models;
using Dapper.Repository.Helpers;

namespace Dapper.Repository.Services
{
    public interface IOrderRespository
    {
        Task<PagedResults<OrderDtoQuery>> GetByCustomerId(int customerId, int page, int pageSize);

        //Task<OrderDtoQuery> GetById(int orderId);

        //Task<OrderDtoQuery> Insert(Order order);

        Task<bool> Update(Order order);

        Task<bool> Delete(int orderId);

        Task<Order> GetEntityById(int orderId);
    }
}
