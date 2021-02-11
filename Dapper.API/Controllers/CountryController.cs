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
    public class CountryController : ControllerBase
    {
        private readonly ICountryRespository countryRespository;
        private readonly IProvinceRespository provinceRespository;
        private readonly IMapper mapper;

        public CountryController(ICountryRespository countryRespository, IProvinceRespository provinceRespository, IMapper mapper)
        { 
            this.countryRespository = countryRespository;
            this.provinceRespository = provinceRespository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<CountryResponseDTO>> GetAll()
        {
            var countries = await countryRespository.GetAll();

            return mapper.Map<IEnumerable<CountryResponseDTO>>(countries);
        }

        [HttpGet("{countryId}")]
        public async Task<ActionResult<CountryResponseDTO>> GetById(int countryId)
        {
            var country = await countryRespository.GetById(countryId);
            if (country is null)
            {
                return NotFound();
            }

            return mapper.Map<CountryResponseDTO>(country);
        }

        [HttpGet("{countryId}/Province")]
        public async Task<IEnumerable<ProvinceResponseDTO>> GetProvinces(int countryId)
        {
            var provinces = await provinceRespository.GetByCountryId(countryId);

            return mapper.Map<IEnumerable<ProvinceResponseDTO>>(provinces);
        }

        [HttpPost]
        public async Task<ActionResult<CountryResponseDTO>> Insert(CountryPostDTO countryPostDTO)
        {
            // Map countryPostDTO to repositories Country entity
            var newCountry = mapper.Map<Country>(countryPostDTO);

            // Apply audit changes to Country entity
            newCountry = Audit<Country>.PerformAudit(newCountry);

            // Insert new Country into the respository
            newCountry = await countryRespository.Insert(newCountry);

            // Map the Country entity to DTO response object and return in body of response
            var countryResponseDTO = mapper.Map<CountryResponseDTO>(newCountry);

            return CreatedAtAction(nameof(GetById), new { countryResponseDTO.CountryId }, countryResponseDTO);
        }

        [HttpPut("{countryId}")]
        public async Task<ActionResult> Update(int countryId, CountryPutDTO dtoCountry)
        {
            if (countryId != dtoCountry.CountryId)
            {
                ModelState.AddModelError("CountryId", "The Parameter CountryId and the CountryId from the body do not match.");
                return ValidationProblem(ModelState);
            }

            // Get a copy of the Country entity from the respository
            var updateCountry = await countryRespository.GetById(countryId);
            if (updateCountry is null)
            {
                return NotFound();
            }

            // Map dtoCountry to the repositories Country entity
            updateCountry = mapper.Map(dtoCountry, updateCountry);

            // Apply audit changes to Country entity
            updateCountry = Audit<Country>.PerformAudit(updateCountry);

            // Update Country in the respository
            var isUpdated = await countryRespository.Update(updateCountry);
            if (!isUpdated)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete("{countryId}")]
        public async Task<ActionResult> Delete(int countryId)
        {
            var isDeleted = await countryRespository.Delete(countryId);
            if (!isDeleted)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
