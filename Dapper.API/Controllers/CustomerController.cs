using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dapper.Models;
using Dapper.Repository;

namespace Dapper.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRespository customerRespository;

        public CustomerController(ICustomerRespository customerRespository)
        {
            this.customerRespository = customerRespository;
        }

        [HttpGet]
        public async Task<IEnumerable<Customer>> Get()
        {
            return await customerRespository.GetAll();
        }

        [HttpGet("{customerId}")]
        public async Task<ActionResult<Customer>> Get(long customerId)
        {
            var result = await customerRespository.GetById(customerId);
            if (result == null)
            {
                return NotFound();
            }

            return result;
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> Post(Customer customer)
        {
            return await customerRespository.Insert(customer);
        }

        [HttpPut("{customerId}")]
        public async Task<ActionResult> Put(long customerId, Customer customer)
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
        public async Task<ActionResult> Delete(long customerId)
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
