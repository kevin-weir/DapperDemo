using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Dapper.Repository.Models;
using Dapper.Domain.Models;
using Dapper.Repository.Interfaces;
using Dapper.API.Helpers;
using AutoMapper;

namespace Dapper.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly ICountryRespository countryRespository;
        private readonly IMapper mapper;
        private readonly IDistributedCache cache;
        private const string cacheKey = nameof(CountryController) + nameof(Get) + nameof(Country);

        public CountryController(ICountryRespository countryRespository, IMapper mapper, IDistributedCache cache)
        { 
            this.countryRespository = countryRespository;
            this.mapper = mapper;
            this.cache = cache;
        }

        [HttpGet]
        public async Task<IEnumerable<CountryDtoQuery>> Get()
        {
            List <CountryDtoQuery> countries;

            var cachedResult = await cache.GetAsync(cacheKey);
            if (cachedResult is not null)
            {
                countries = Cache<List<CountryDtoQuery>>.ByteArrayToObject(cachedResult);
            }
            else
            {
                countries = (List<CountryDtoQuery>)await countryRespository.GetAll();
                await cache.SetAsync(cacheKey, Cache<List<CountryDtoQuery>>.ObjectToByteArray(countries));
            }

            return countries;

            //return await countryRespository.GetAll();
        }

        [HttpGet("{countryId}")]
        public async Task<ActionResult<CountryDtoQuery>> Get(int countryId)
        {
            var country = await countryRespository.GetById(countryId);
            if (country is null)
            {
                return NotFound();
            }

            return country;
        }

        [HttpPost]
        public async Task<ActionResult<CountryDtoQuery>> Post(CountryDtoInsert dtoCountry)
        {
            // Map dtoCountry to repositories Country entity
            var newCountry = mapper.Map<Country>(dtoCountry);

            // Apply audit changes to Country entity
            newCountry = Audit<Country>.PerformAudit(newCountry);

            // Validate Country entity using ModelState
            if (!TryValidateModel(newCountry))
            {
                return ValidationProblem(ModelState);
            }

            // Insert new Country into the respository
            var countryDtoQuery = await countryRespository.Insert(newCountry);

            // Remove the cached results using cacheKey
            await cache.RemoveAsync(cacheKey);

            return CreatedAtAction(nameof(Get), new { countryDtoQuery.CountryId }, countryDtoQuery);
        }

        [HttpPut("{countryId}")]
        public async Task<ActionResult> Put(int countryId, CountryDtoUpdate dtoCountry)
        {
            if (countryId != dtoCountry.CountryId)
            {
                ModelState.AddModelError("CountryId", "The Parameter CountryId and the CountryId from the body do not match.");
                return ValidationProblem(ModelState);
            }

            // Get a copy of the Country entity from the respository
            var updateCountry = await countryRespository.GetEntityById(countryId);
            if (updateCountry is null)
            {
                return NotFound();
            }

            // Map dtoCountry to the repositories Country entity
            updateCountry = mapper.Map(dtoCountry, updateCountry);

            // Apply audit changes to Country entity
            updateCountry = Audit<Country>.PerformAudit(updateCountry);

            // Validate Country entity using ModelState
            if (!TryValidateModel(updateCountry))
            {
                return ValidationProblem(ModelState);
            }

            // Update Country in the respository
            var isUpdated = await countryRespository.Update(updateCountry);
            if (!isUpdated)
            {
                return NotFound();
            }

            // Remove the cached results using cacheKey
            await cache.RemoveAsync(cacheKey);

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

            // Remove the cached results using cacheKey
            await cache.RemoveAsync(cacheKey);

            return Ok();
        }
    }
}
