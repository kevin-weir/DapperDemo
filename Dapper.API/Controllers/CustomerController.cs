using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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

        // TODO: ActionResult

        // 200 Success  204 No Content or 404 Not Found  500 Server Error
        // GET: /Customer
        [HttpGet]
        public async Task<IEnumerable<Customer>> Get()
        {
            return await customerRespository.GetAll();
        }

        // 200 Success  204 No Content or 404 Not Found  500 Server Error
        // GET /Customer/5
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

        // 201 Success  400 Bad Request  500 Server Error
        // POST /Customer
        [HttpPost]
        public async Task<ActionResult<Customer>> Post(Customer customer)
        {
            return await customerRespository.Insert(customer);
        }

        // 200 Success  400 Bad Request  404 Not Found  409 Conflict  500 Server Error
        // PUT /Customer/5
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

        // 200 Success  404 Not Found  500 Server Error
        // DELETE /Customer/5
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
