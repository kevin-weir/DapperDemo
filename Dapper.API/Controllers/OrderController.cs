using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Dapper.Repository.Models;
using Dapper.Repository.Services;
using Dapper.Domain.Models;
using Dapper.API.Helpers;

namespace Dapper.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRespository orderRespository;
        private readonly IMapper mapper;

        public OrderController(IOrderRespository orderRespository, IMapper mapper)
        {
            this.orderRespository = orderRespository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<PagedResults<OrderResponseDTO>> Get(int page = 1, int pageSize = 10)
        {
            var pagedResults = await orderRespository.GetAll(page, pageSize);

            return mapper.Map<PagedResults<OrderResponseDTO>>(pagedResults);
        }

        [HttpGet("{orderId}")]
        public async Task<ActionResult<OrderResponseDTO>> Get(int orderId)
        {
            var order = await orderRespository.GetById(orderId);
            if (order is null)
            {
                return NotFound();
            }

            return mapper.Map<OrderResponseDTO>(order);
        }

        [HttpPost]
        public async Task<ActionResult<OrderResponseDTO>> Post(OrderPostDTO orderPostDTO)
        {
            // Map orderPostDTO to repositories Order entity
            var newOrder = mapper.Map<Order>(orderPostDTO);

            // Apply audit changes to Order entity
            newOrder = Audit<Order>.PerformAudit(newOrder);

            // Insert new Order into the respository
            newOrder = await orderRespository.Insert(newOrder);

            // Map the Order entity to DTO response object and return in body of response
            var orderResponseDTO = mapper.Map<OrderResponseDTO>(newOrder);

            return CreatedAtAction(nameof(Get), new { orderResponseDTO.OrderId }, orderResponseDTO);
        }

        [HttpPut("{orderId}")]
        public async Task<ActionResult> Put(int orderId, OrderPutDTO orderPutDTO)
        {
            if (orderId != orderPutDTO.OrderId)
            {
                ModelState.AddModelError("OrderId", "The Parameter OrderId and the OrderId from the body do not match.");
                return ValidationProblem(ModelState);
            }

            // Get a copy of the Order entity from the respository
            var updateOrder = await orderRespository.GetById(orderId);
            if (updateOrder is null)
            {
                return NotFound();
            }

            // Map orderPutDTO to the repositories Order entity
            updateOrder = mapper.Map(orderPutDTO, updateOrder);

            // Apply audit changes to Order entity
            updateOrder = Audit<Order>.PerformAudit(updateOrder);

            // Update Order in the respository
            var isUpdated = await orderRespository.Update(updateOrder);
            if (!isUpdated)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete("{orderId}")]
        public async Task<ActionResult> Delete(int orderId)
        {
            var isDeleted = await orderRespository.Delete(orderId);
            if (!isDeleted)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
