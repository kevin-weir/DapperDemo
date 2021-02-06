using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dapper.Repository.Models;
using Dapper.Domain.Models;
using Dapper.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using System;
using Dapper.API.Helpers;

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
            return await customerRespository.GetAll();
        }

        [HttpGet("{customerId}")]
        public async Task<ActionResult<CustomerDtoQuery>> Get(int customerId)
        {
            var result = await customerRespository.GetById(customerId);
            if (result == null)
            {
                return NotFound();
            }

            return result;
        }

        [HttpPost]
        //[ProducesResponseType(typeof(CustomerDTO), StatusCodes.Status201Created)]
        public async Task<ActionResult<CustomerDtoQuery>> Post(CustomerDtoInsert dtoCustomer)
        {
            // Map the posted dtoCustomer to the repositories Customer entity
            var newCustomer = mapper.Map<Customer>(dtoCustomer);

            // TODO Perform validation on modelstate

            // Audit the changes made to the new customer
            newCustomer = Audit<Customer>.PerformAudit(newCustomer);

            // Insert the new Customer into the respository
            var customerDtoQuery = await customerRespository.Insert(newCustomer);

            return CreatedAtAction(nameof(Get), new { customerDtoQuery.CustomerId }, customerDtoQuery);
        }

        [HttpPut("{customerId}")]
        public async Task<ActionResult> Put(int customerId, Customer customer)
        {
            if (customerId != customer.CustomerId)
            {
                ModelState.AddModelError("CustomerId", "The Parameter CustomerId and the CustomerId from the body do not match."); 
                return ValidationProblem(ModelState);
            }

            var isUpdated = await customerRespository.Update(customer);
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
