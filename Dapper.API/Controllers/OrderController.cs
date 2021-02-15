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
        private readonly IOrderRespository _orderRespository;
        private readonly IMapper _mapper;

        public OrderController(IOrderRespository orderRespository, IMapper mapper)
        {
            _orderRespository = orderRespository;
            _mapper = mapper;
        }

        [HttpGet(Name = nameof(OrderGetAll))]
        public async Task<PagedResults<OrderResponseDTO>> OrderGetAll([FromQuery] PagingParameters pagingParameters)
        {
            var pagedResults = await _orderRespository.GetAll(pagingParameters.Page, pagingParameters.PageSize);

            return _mapper.Map<PagedResults<OrderResponseDTO>>(pagedResults);
        }

        [HttpGet("{orderId}", Name = nameof(OrderGetById))]
        public async Task<ActionResult<OrderResponseDTO>> OrderGetById(int orderId)
        {
            var order = await _orderRespository.GetById(orderId);
            if (order is null)
            {
                return NotFound();
            }

            return _mapper.Map<OrderResponseDTO>(order);
        }

        [HttpPost(Name = nameof(OrderInsert))]
        public async Task<ActionResult<OrderResponseDTO>> OrderInsert(OrderPostDTO orderPostDTO)
        {
            // Map orderPostDTO to repositories Order entity
            var newOrder = _mapper.Map<Order>(orderPostDTO);

            // Apply audit changes to Order entity
            newOrder = Audit<Order>.PerformAudit(newOrder);

            // Insert new Order into the respository
            newOrder = await _orderRespository.Insert(newOrder);

            // Map the Order entity to DTO response object and return in body of response
            var orderResponseDTO = _mapper.Map<OrderResponseDTO>(newOrder);

            return CreatedAtAction(nameof(OrderGetById), new { orderResponseDTO.OrderId }, orderResponseDTO);
        }

        [HttpPut("{orderId}", Name = nameof(OrderUpdate))]
        public async Task<ActionResult> OrderUpdate(int orderId, OrderPutDTO orderPutDTO)
        {
            if (orderId != orderPutDTO.OrderId)
            {
                ModelState.AddModelError("OrderId", "The Parameter OrderId and the OrderId from the body do not match.");
                return ValidationProblem(ModelState);
            }

            // Get a copy of the Order entity from the respository
            var updateOrder = await _orderRespository.GetById(orderId);
            if (updateOrder is null)
            {
                return NotFound();
            }

            // Map orderPutDTO to the repositories Order entity
            updateOrder = _mapper.Map(orderPutDTO, updateOrder);

            // Apply audit changes to Order entity
            updateOrder = Audit<Order>.PerformAudit(updateOrder);

            // Update Order in the respository
            var isUpdated = await _orderRespository.Update(updateOrder);
            if (!isUpdated)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete("{orderId}", Name = nameof(OrderDelete))]
        public async Task<ActionResult> OrderDelete(int orderId)
        {
            var isDeleted = await _orderRespository.Delete(orderId);
            if (!isDeleted)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
