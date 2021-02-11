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
    public class OrderController : ControllerBase
    {
        private readonly IOrderRespository orderRespository;
        private readonly IMapper mapper;

        public OrderController(IOrderRespository orderRespository, IMapper mapper)
        {
            this.orderRespository = orderRespository;
            this.mapper = mapper;
        }

        //[HttpGet("paging")]
        //public async Task<PagedResults<OrderDtoQuery>> GetByCustomerId(int customerId, int page, int pageSize)
        //{
        //    var orders = await orderRespository.GetByCustomerId(customerId, page, pageSize);

        //    return null;
        //    //return await customerRespository.GetAll();
        //}

        //[HttpGet("{customerId}")]
        //public async Task<ActionResult<CustomerDtoQuery>> Get(int customerId)
        //{
        //    var customer = await customerRespository.GetById(customerId);
        //    if (customer is null)
        //    {
        //        return NotFound();
        //    }

        //    return customer;
        //}

        //[HttpPost]
        //public async Task<ActionResult<CustomerDtoQuery>> Post(CustomerDtoInsert dtoCustomer)
        //{
        //    // Map dtoCustomer to repositories Customer entity
        //    var newCustomer = mapper.Map<Customer>(dtoCustomer);

        //    // Apply audit changes to Customer entity
        //    newCustomer = Audit<Customer>.PerformAudit(newCustomer);

        //    // Validate Customer entity using ModelState
        //    if (!TryValidateModel(newCustomer))
        //    {
        //        return ValidationProblem(ModelState);
        //    }

        //    // Insert new Customer into the respository
        //    var customerDtoQuery = await customerRespository.Insert(newCustomer);

        //    return CreatedAtAction(nameof(Get), new { customerDtoQuery.CustomerId }, customerDtoQuery);
        //}

        //[HttpPut("{customerId}")]
        //public async Task<ActionResult> Put(int customerId, CustomerDtoUpdate dtoCustomer)
        //{
        //    if (customerId != dtoCustomer.CustomerId)
        //    {
        //        ModelState.AddModelError("CustomerId", "The Parameter CustomerId and the CustomerId from the body do not match.");
        //        return ValidationProblem(ModelState);
        //    }

        //    // Get a copy of the Customer entity from the respository
        //    var updateCustomer = await customerRespository.GetEntityById(customerId);
        //    if (updateCustomer is null) 
        //    {
        //        return NotFound();
        //    }

        //    // Map dtoCustomer to the repositories Customer entity
        //    updateCustomer = mapper.Map(dtoCustomer, updateCustomer);

        //    // Apply audit changes to Customer entity
        //    updateCustomer = Audit<Customer>.PerformAudit(updateCustomer);

        //    // Validate Customer entity using ModelState
        //    if (!TryValidateModel(updateCustomer))
        //    {
        //        return ValidationProblem(ModelState);
        //    }

        //    // Update Customer in the respository
        //    var isUpdated = await customerRespository.Update(updateCustomer);
        //    if (!isUpdated) 
        //    {
        //        return NotFound();
        //    }

        //    return Ok();
        //}

        //[HttpDelete("{customerId}")]
        //public async Task<ActionResult> Delete(int customerId)
        //{
        //    var isDeleted = await customerRespository.Delete(customerId);
        //    if (!isDeleted) 
        //    {
        //        return NotFound();
        //    }

        //    return Ok();
        //}
    }
}
