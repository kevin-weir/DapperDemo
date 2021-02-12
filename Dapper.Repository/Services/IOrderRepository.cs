using System.Threading.Tasks;
using Dapper.Domain.Models;
using Dapper.Repository.Models;

namespace Dapper.Repository.Services
{
    public interface IOrderRespository
    {
        Task<PagedResults<Order>> GetAll(int page = 1, int pageSize = 10);

        Task<Order> GetById(int orderId);

        Task<PagedResults<Order>> GetByCustomerId(int customerId, int page, int pageSize);

        Task<Order> Insert(Order order);

        Task<bool> Update(Order order);

        Task<bool> Delete(int orderId);
    }
}
