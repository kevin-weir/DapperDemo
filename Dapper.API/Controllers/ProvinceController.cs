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
        private readonly IProvinceRespository _provinceRespository;
        private readonly IMapper _mapper;

        public ProvinceController(IProvinceRespository provinceRespository, IMapper mapper)
        {
            _provinceRespository = provinceRespository;
            _mapper = mapper;
        }

        [HttpGet(Name = nameof(ProvinceGetAll))]
        public async Task<IEnumerable<ProvinceResponseDTO>> ProvinceGetAll()
        {
            var provinces = await _provinceRespository.GetAll();

            return _mapper.Map<IEnumerable<ProvinceResponseDTO>>(provinces);
        }

        [HttpGet("{provinceId}", Name = nameof(ProvinceGetById))]
        public async Task<ActionResult<ProvinceResponseDTO>> ProvinceGetById(int provinceId)
        {
            var province = await _provinceRespository.GetById(provinceId);
            if (province is null)
            {
                return NotFound();
            }

            return _mapper.Map<ProvinceResponseDTO>(province);
        }

        [HttpPost(Name = nameof(ProvinceInsert))]
        public async Task<ActionResult<ProvinceResponseDTO>> ProvinceInsert(ProvincePostDTO provincePostDTO)
        {
            // Map provincePostDTO to repositories Province entity
            var newProvince = _mapper.Map<Province>(provincePostDTO);

            // Apply audit changes to Province entity
            newProvince = Audit<Province>.PerformAudit(newProvince);

            // Insert new Province into the respository
            newProvince = await _provinceRespository.Insert(newProvince);

            // Map the Province entity to DTO response object and return in body of response
            var provinceResponseDTO = _mapper.Map<ProvinceResponseDTO>(newProvince);

            return CreatedAtAction(nameof(ProvinceGetById), new { provinceResponseDTO.ProvinceId }, provinceResponseDTO);
        }

        [HttpPut("{provinceId}", Name = nameof(ProvinceUpdate))]
        public async Task<ActionResult> ProvinceUpdate(int provinceId, ProvincePutDTO provincePutDTO)
        {
            if (provinceId != provincePutDTO.ProvinceId)
            {
                ModelState.AddModelError("ProvinceId", "The Parameter ProvinceId and the ProvinceId from the body do not match.");
                return ValidationProblem(ModelState);
            }

            // Get a copy of the Province entity from the respository
            var updateProvince = await _provinceRespository.GetById(provinceId);
            if (updateProvince is null)
            {
                return NotFound();
            }

            // Map provincePutDTO to the repositories Province entity
            updateProvince = _mapper.Map(provincePutDTO, updateProvince);

            // Apply audit changes to Province entity
            updateProvince = Audit<Province>.PerformAudit(updateProvince);

            // Update Province in the respository
            var isUpdated = await _provinceRespository.Update(updateProvince);
            if (!isUpdated)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete("{provinceId}", Name = nameof(ProvinceDelete))]
        public async Task<ActionResult> ProvinceDelete(int provinceId)
        {
            var isDeleted = await _provinceRespository.Delete(provinceId);
            if (!isDeleted)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
