using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NewZealandWalks.Api.CustomActionFilters;
using NewZealandWalks.Api.Data;
using NewZealandWalks.Api.Models.Domain;
using NewZealandWalks.Api.Models.DTO;
using NewZealandWalks.Api.Repositories;

namespace NewZealandWalks.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _dbContext;
        private readonly IRegionRepository _regionRepository;

        public RegionsController(IMapper mapper, ApplicationDbContext dbContext, IRegionRepository regionRepository)
        {
            _mapper = mapper;
            this._dbContext = dbContext;
            this._regionRepository = regionRepository;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> GetAllRegions()
        {
            var Regions = await _regionRepository.GetAllAsync();
            var regionsDto = _mapper.Map<List<RegionDTO>>(Regions);
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
        [ValidateModel]
        [Route("CreateRegion", Name = "CreateAsync")]
        public async Task<IActionResult> CreateAsync([FromBody] AddRegionRequestDTO regionDTO)
        {
            if (ModelState.IsValid)
            {
                var region = _mapper.Map<Region>(regionDTO);
                var domainModel = await _regionRepository.CreateAsync(region);
                var regionDto = _mapper.Map<RegionDTO>(domainModel);
                return CreatedAtAction("getbyid", new { id = regionDto.Id }, regionDTO);
            }
            else
                return BadRequest();

        }
        [HttpPut]
        [ValidateModel]

        [Route("Updateregion/{id:guid}", Name = "UpadateRegion")]
        public async Task<IActionResult> Updateregion([FromRoute] Guid id, [FromBody] UpdateRegionRequestDTO updateRegionDto)
        {
            
                var region = _mapper.Map<Region>(updateRegionDto);
            var updatedModel = await _regionRepository.UpdateAsync(id, region);
            var regoinDto = _mapper.Map<RegionDTO>(region);
            return Ok(regoinDto);
                return BadRequest();
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var regionDomainModel = await _regionRepository.DeleteAsync(id);
            if (regionDomainModel is null)
            { return NotFound(); }
            var regoinDto = _mapper.Map<RegionDTO>(regionDomainModel);
            return Ok(regoinDto);
        }
    }
}
