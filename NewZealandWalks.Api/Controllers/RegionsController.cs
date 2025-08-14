using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewZealandWalks.Api.Data;
using NewZealandWalks.Api.Models.Domain;
using NewZealandWalks.Api.Models.DTO;
using NewZealandWalks.Api.Repositories;
using System.Globalization;

namespace NewZealandWalks.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IRegionRepository _regionRepository;

        public RegionsController(ApplicationDbContext dbContext,IRegionRepository regionRepository)
        {
            this._dbContext = dbContext;
            this._regionRepository = regionRepository;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> GetAllRegions()
        {
            var Regions = await _regionRepository.GetAllAsync();
            var regionsDto = new List<RegionDTO>();
            foreach (var obj in Regions)
            {
                regionsDto.Add(new RegionDTO()
                {
                    Id = obj.Id,
                    Name = obj.Name,
                    Code = obj.Code,
                    RegionImageUrl = obj.RegionImageUrl
                });
            }
            return Ok(regionsDto);
        }
        [HttpGet]
        [Route("getbyid/{id:guid}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            var region = await _regionRepository.GetByIdAsync(id);
            if (region == null)
            {
                return NotFound();
            }
            var regionDto = new RegionDTO
            {
                Id = region.Id,
                Name = region.Name,
                Code = region.Code,
                RegionImageUrl = region.RegionImageUrl
            };
            return Ok(regionDto);
        }
        [HttpPost]
        [Route("CreateRegion",Name="CreateAsync")]
        public async Task<IActionResult> CreateAsync([FromBody] AddRegionRequestDTO regionDTO)
        {
            if (regionDTO is null)
            {
                return BadRequest();
            }
            Region region = new Region
            {
                Name = regionDTO.Name,
                Code = regionDTO.Code,
                RegionImageUrl = regionDTO.RegionImageUrl
            };
            await _dbContext.AddAsync(region);
            await _dbContext.SaveChangesAsync();
            var regionDto = new RegionDTO
            {
                Id = region.Id,
                Name = regionDTO.Name,
                Code = regionDTO.Code,
                RegionImageUrl = regionDTO.RegionImageUrl
            };
            return CreatedAtAction("getbyid", new { id = regionDto.Id }, regionDTO);
        }
        [HttpPut]
        [Route("Updateregion/{id:guid}", Name = "UpadateRegion")]
        public async Task<IActionResult> Updateregion([FromRoute] Guid id, [FromBody] UpdateRegionRequestDTO updateRegionDto)
        {
            var region = new Region()
            {
                Name = updateRegionDto.Name,
                Code = updateRegionDto.Code,
                RegionImageUrl = updateRegionDto.RegionImageUrl
            };
            var updatedModel= await  _regionRepository.UpdateAsync(id, region);   
            if (updatedModel is null)
            { return NotFound(); }
            var regoinDto = new RegionDTO
            {
                Id = updatedModel.Id,
                Name = updatedModel.Name,
                Code = updatedModel.Code,
                RegionImageUrl = updatedModel.RegionImageUrl
            };
            return Ok(regoinDto);
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var regionDomainModel =await _regionRepository.DeleteAsync(id);
            if (regionDomainModel is null)
            { return NotFound(); }
            var regoinDto = new RegionDTO
            {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            return Ok(regoinDto);
        }
    }
}
