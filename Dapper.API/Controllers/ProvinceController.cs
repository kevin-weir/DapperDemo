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
    public class ProvinceController : ControllerBase
    {
        private readonly IProvinceRespository provinceRespository;
        private readonly IMapper mapper;

        public ProvinceController(IProvinceRespository provinceRespository, IMapper mapper)
        {
            this.provinceRespository = provinceRespository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ProvinceDtoQuery>> Get()
        {
            var provinces = await provinceRespository.GetAll();

            return mapper.Map<IEnumerable<ProvinceDtoQuery>>(provinces);
        }

        [HttpGet("{provinceId}")]
        public async Task<ActionResult<ProvinceDtoQuery>> Get(int provinceId)
        {
            var province = await provinceRespository.GetById(provinceId);
            if (province is null)
            {
                return NotFound();
            }

            return mapper.Map<ProvinceDtoQuery>(province);
        }

        [HttpPost]
        public async Task<ActionResult<ProvinceDtoQuery>> Post(ProvinceDtoInsert dtoProvince)
        {
            // Map dtoProvince to repositories Province entity
            var newProvince = mapper.Map<Province>(dtoProvince);

            // Apply audit changes to Province entity
            newProvince = Audit<Province>.PerformAudit(newProvince);

            // Insert new Province into the respository
            newProvince = await provinceRespository.Insert(newProvince);

            // Map the Province entity to DTO response object and return in body of response
            var provinceDtoQuery = mapper.Map<ProvinceDtoQuery>(newProvince);

            return CreatedAtAction(nameof(Get), new { provinceDtoQuery.ProvinceId }, provinceDtoQuery);
        }

        [HttpPut("{provinceId}")]
        public async Task<ActionResult> Put(int provinceId, ProvinceDtoUpdate dtoProvince)
        {
            if (provinceId != dtoProvince.ProvinceId)
            {
                ModelState.AddModelError("ProvinceId", "The Parameter ProvinceId and the ProvinceId from the body do not match.");
                return ValidationProblem(ModelState);
            }

            // Get a copy of the Province entity from the respository
            var updateProvince = await provinceRespository.GetById(provinceId);
            if (updateProvince is null)
            {
                return NotFound();
            }

            // Map dtoProvince to the repositories Province entity
            updateProvince = mapper.Map(dtoProvince, updateProvince);

            // Apply audit changes to Province entity
            updateProvince = Audit<Province>.PerformAudit(updateProvince);

            // Update Province in the respository
            var isUpdated = await provinceRespository.Update(updateProvince);
            if (!isUpdated)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete("{provinceId}")]
        public async Task<ActionResult> Delete(int provinceId)
        {
            var isDeleted = await provinceRespository.Delete(provinceId);
            if (!isDeleted)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
