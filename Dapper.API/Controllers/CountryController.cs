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
        private readonly ICountryRespository _countryRespository;
        private readonly IProvinceRespository _provinceRespository;
        private readonly IMapper _mapper;

        public CountryController(ICountryRespository countryRespository, IProvinceRespository provinceRespository, IMapper mapper)
        { 
            _countryRespository = countryRespository;
            _provinceRespository = provinceRespository;
            _mapper = mapper;
        }

        [HttpGet(Name = nameof(CountryGetAll))]
        public async Task<IEnumerable<CountryResponseDTO>> CountryGetAll()
        {
            var countries = await _countryRespository.GetAll();

            return _mapper.Map<IEnumerable<CountryResponseDTO>>(countries);
        }

        [HttpGet("{countryId}", Name = nameof(CountryGetById))]
        public async Task<ActionResult<CountryResponseDTO>> CountryGetById(int countryId)
        {
            var country = await _countryRespository.GetById(countryId);
            if (country is null)
            {
                return NotFound();
            }

            return _mapper.Map<CountryResponseDTO>(country);
        }

        [HttpGet("{countryId}/Province", Name = nameof(CountryGetProvinces))]
        public async Task<IEnumerable<ProvinceResponseDTO>> CountryGetProvinces(int countryId)
        {
            var provinces = await _provinceRespository.GetByCountryId(countryId);

            return _mapper.Map<IEnumerable<ProvinceResponseDTO>>(provinces);
        }

        [HttpPost(Name = nameof(CountryInsert))]
        public async Task<ActionResult<CountryResponseDTO>> CountryInsert(CountryPostDTO countryPostDTO)
        {
            // Map countryPostDTO to repositories Country entity
            var newCountry = _mapper.Map<Country>(countryPostDTO);

            // Apply audit changes to Country entity
            newCountry = Audit<Country>.PerformAudit(newCountry);

            // Insert new Country into the respository
            newCountry = await _countryRespository.Insert(newCountry);

            // Map the Country entity to DTO response object and return in body of response
            var countryResponseDTO = _mapper.Map<CountryResponseDTO>(newCountry);

            return CreatedAtAction(nameof(CountryGetById), new { countryResponseDTO.CountryId }, countryResponseDTO);
        }

        [HttpPut("{countryId}", Name = nameof(CountryUpdate))]
        public async Task<ActionResult> CountryUpdate(int countryId, CountryPutDTO countryPutDTO)
        {
            if (countryId != countryPutDTO.CountryId)
            {
                ModelState.AddModelError("CountryId", "The Parameter CountryId and the CountryId from the body do not match.");
                return ValidationProblem(ModelState);
            }

            // Get a copy of the Country entity from the respository
            var updateCountry = await _countryRespository.GetById(countryId);
            if (updateCountry is null)
            {
                return NotFound();
            }

            // Map countryPutDTO to the repositories Country entity
            updateCountry = _mapper.Map(countryPutDTO, updateCountry);

            // Apply audit changes to Country entity
            updateCountry = Audit<Country>.PerformAudit(updateCountry);

            // Update Country in the respository
            var isUpdated = await _countryRespository.Update(updateCountry);
            if (!isUpdated)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete("{countryId}", Name = nameof(CountryDelete))]
        public async Task<ActionResult> CountryDelete(int countryId)
        {
            var isDeleted = await _countryRespository.Delete(countryId);
            if (!isDeleted)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
