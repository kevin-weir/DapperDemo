using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dapper.Repository.Models;
using Dapper.Domain.Models;
using Dapper.Repository.Interfaces;
using Dapper.API.Helpers;
using AutoMapper;

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
            return await provinceRespository.GetAll();
        }

        [HttpGet("{provinceId}")]
        public async Task<ActionResult<ProvinceDtoQuery>> Get(int provinceId)
        {
            var province = await provinceRespository.GetById(provinceId);
            if (province is null)
            {
                return NotFound();
            }

            return province;
        }

        [HttpGet("country/{countryId}")]
        public async Task<IEnumerable<ProvinceDtoQuery>> GetByCountryId(int countryId)
        {
            return await provinceRespository.GetByCountryId(countryId);
        }

        [HttpPost]
        public async Task<ActionResult<ProvinceDtoQuery>> Post(ProvinceDtoInsert dtoProvince)
        {
            // Map dtoProvince to repositories Province entity
            var newProvince = mapper.Map<Province>(dtoProvince);

            // Apply audit changes to Province entity
            newProvince = Audit<Province>.PerformAudit(newProvince);

            // Validate Province entity using ModelState
            if (!TryValidateModel(newProvince))
            {
                return ValidationProblem(ModelState);
            }

            // Insert new Province into the respository
            var provinceDtoQuery = await provinceRespository.Insert(newProvince);

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
            var updateProvince = await provinceRespository.GetEntityById(provinceId);
            if (updateProvince is null)
            {
                return NotFound();
            }

            // Map dtoProvince to the repositories Province entity
            updateProvince = mapper.Map(dtoProvince, updateProvince);

            // Apply audit changes to Province entity
            updateProvince = Audit<Province>.PerformAudit(updateProvince);

            // Validate Province entity using ModelState
            if (!TryValidateModel(updateProvince))
            {
                return ValidationProblem(ModelState);
            }

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
