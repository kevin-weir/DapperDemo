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
        private readonly ICustomerRespository customerRespository;
        private readonly IOrderRespository orderRespository;
        private readonly IMapper mapper;

        public CustomerController(ICustomerRespository customerRespository, IOrderRespository orderRespository, IMapper mapper)
        {
            this.customerRespository = customerRespository;
            this.orderRespository = orderRespository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<CustomerResponseDTO>> Get()
        {
            var customers = await customerRespository.GetAll();

            return mapper.Map<IEnumerable<CustomerResponseDTO>>(customers);
        }

        [HttpGet("{customerId}")]
        public async Task<ActionResult<CustomerResponseDTO>> Get(int customerId)
        {
            var customer = await customerRespository.GetById(customerId);
            if (customer is null)
            {
                return NotFound();
            }

            return mapper.Map<CustomerResponseDTO>(customer);
        }

        [HttpGet("{customerId}/Order")]
        public async Task<PagedResults<OrderResponseDTO>> GetOrders(int customerId, int page = 1, int pageSize = 10)
       {
            //var orders = await orderRespository.GetByCustomerId(customerId, page, pageSize);
            //return mapper.Map<IEnumerable<ProvinceDtoQuery>>(provinces);
            return null;
        }

        [HttpPost]
        public async Task<ActionResult<CustomerResponseDTO>> Post(CustomerPostDTO customerPostDTO)
        {
            // Map customerPostDTO to repositories Customer entity
            var newCustomer = mapper.Map<Customer>(customerPostDTO);

            // Apply audit changes to Customer entity
            newCustomer = Audit<Customer>.PerformAudit(newCustomer);

            // Insert new Customer into the respository
            newCustomer = await customerRespository.Insert(newCustomer);

            // Map the Customer entity to DTO response object and return in body of response
            var customerResponseDTO = mapper.Map<CustomerResponseDTO>(newCustomer);

            return CreatedAtAction(nameof(Get), new { customerResponseDTO.CustomerId }, customerResponseDTO);
        }

        [HttpPut("{customerId}")]
        public async Task<ActionResult> Put(int customerId, CustomerPutDTO dtoCustomer)
        {
            if (customerId != dtoCustomer.CustomerId)
            {
                ModelState.AddModelError("CustomerId", "The Parameter CustomerId and the CustomerId from the body do not match.");
                return ValidationProblem(ModelState);
            }

            // Get a copy of the Customer entity from the respository
            var updateCustomer = await customerRespository.GetById(customerId);
            if (updateCustomer is null) 
            {
                return NotFound();
            }

            // Map dtoCustomer to the repositories Customer entity
            updateCustomer = mapper.Map(dtoCustomer, updateCustomer);

            // Apply audit changes to Customer entity
            updateCustomer = Audit<Customer>.PerformAudit(updateCustomer);

            // Update Customer in the respository
            var isUpdated = await customerRespository.Update(updateCustomer);
            if (!isUpdated) 
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete("{customerId}")]
        public async Task<ActionResult> Delete(int customerId)
        {
            var isDeleted = await customerRespository.Delete(customerId);
            if (!isDeleted) 
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
