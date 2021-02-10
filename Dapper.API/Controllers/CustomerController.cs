using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dapper.Repository.Models;
using Dapper.Repository.Services;
using Dapper.Domain.Models;
using Dapper.API.Helpers;
using AutoMapper;

namespace Dapper.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRespository customerRespository;
        private readonly IMapper mapper;

        public CustomerController(ICustomerRespository customerRespository, IMapper mapper)
        {
            this.customerRespository = customerRespository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<CustomerDtoQuery>> Get()
        {
            var customers = await customerRespository.GetAll();

            return mapper.Map<IEnumerable<CustomerDtoQuery>>(customers);
        }

        [HttpGet("{customerId}")]
        public async Task<ActionResult<CustomerDtoQuery>> Get(int customerId)
        {
            var customer = await customerRespository.GetById(customerId);
            if (customer is null)
            {
                return NotFound();
            }

            return mapper.Map<CustomerDtoQuery>(customer);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDtoQuery>> Post(CustomerDtoInsert dtoCustomer)
        {
            // Map dtoCustomer to repositories Customer entity
            var newCustomer = mapper.Map<Customer>(dtoCustomer);

            // Apply audit changes to Customer entity
            newCustomer = Audit<Customer>.PerformAudit(newCustomer);

            // Insert new Customer into the respository
            newCustomer = await customerRespository.Insert(newCustomer);

            // Map the Customer entity to DTO response object and return in body of response
            var customerDtoQuery = mapper.Map<CustomerDtoQuery>(newCustomer);

            return CreatedAtAction(nameof(Get), new { customerDtoQuery.CustomerId }, customerDtoQuery);
        }

        [HttpPut("{customerId}")]
        public async Task<ActionResult> Put(int customerId, CustomerDtoUpdate dtoCustomer)
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
