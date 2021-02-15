using System.Collections.Generic;
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
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRespository _customerRespository;
        private readonly IOrderRespository _orderRespository;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerRespository customerRespository, IOrderRespository orderRespository, IMapper mapper)
        {
            _customerRespository = customerRespository;
            _orderRespository = orderRespository;
            _mapper = mapper;
        }

        [HttpGet(Name = nameof(CustomerGetAll))]
        public async Task<IEnumerable<CustomerResponseDTO>> CustomerGetAll()
        {
            var customers = await _customerRespository.GetAll();

            return _mapper.Map<IEnumerable<CustomerResponseDTO>>(customers);
        }

        [HttpGet("{customerId}", Name = nameof(CustomerGetById))]
        public async Task<ActionResult<CustomerResponseDTO>> CustomerGetById(int customerId)
        {
            var customer = await _customerRespository.GetById(customerId);
            if (customer is null)
            {
                return NotFound();
            }

            return _mapper.Map<CustomerResponseDTO>(customer);
        }

        [HttpGet("{customerId}/Order", Name = nameof(CustomerGetOrders))]

        public async Task<PagedResults<OrderResponseDTO>> CustomerGetOrders(int customerId, [FromQuery] PagingParameters pagingParameters)
        {
            var pagedResults = await _orderRespository.GetByCustomerId(customerId, pagingParameters.Page, pagingParameters.PageSize);

            return _mapper.Map<PagedResults<OrderResponseDTO>>(pagedResults);
        }

        [HttpPost(Name = nameof(CustomerInsert))]
        public async Task<ActionResult<CustomerResponseDTO>> CustomerInsert(CustomerPostDTO customerPostDTO)
        {
            // Map customerPostDTO to repositories Customer entity
            var newCustomer = _mapper.Map<Customer>(customerPostDTO);

            // Apply audit changes to Customer entity
            newCustomer = Audit<Customer>.PerformAudit(newCustomer);

            // Insert new Customer into the respository
            newCustomer = await _customerRespository.Insert(newCustomer);

            // Map the Customer entity to DTO response object and return in body of response
            var customerResponseDTO = _mapper.Map<CustomerResponseDTO>(newCustomer);

            return CreatedAtAction(nameof(CustomerGetById), new { customerResponseDTO.CustomerId }, customerResponseDTO);
        }

        [HttpPut("{customerId}", Name = nameof(CustomerUpdate))]
        public async Task<ActionResult> CustomerUpdate(int customerId, CustomerPutDTO customerPutDTO)
        {
            if (customerId != customerPutDTO.CustomerId)
            {
                ModelState.AddModelError("CustomerId", "The Parameter CustomerId and the CustomerId from the body do not match.");
                return ValidationProblem(ModelState);
            }

            // Get a copy of the Customer entity from the respository
            var updateCustomer = await _customerRespository.GetById(customerId);
            if (updateCustomer is null) 
            {
                return NotFound();
            }

            // Map customerPutDTO to the repositories Customer entity
            updateCustomer = _mapper.Map(customerPutDTO, updateCustomer);

            // Apply audit changes to Customer entity
            updateCustomer = Audit<Customer>.PerformAudit(updateCustomer);

            // Update Customer in the respository
            var isUpdated = await _customerRespository.Update(updateCustomer);
            if (!isUpdated) 
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete("{customerId}", Name = nameof(CustomerDelete))]
        public async Task<ActionResult> CustomerDelete(int customerId)
        {
            var isDeleted = await _customerRespository.Delete(customerId);
            if (!isDeleted) 
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
