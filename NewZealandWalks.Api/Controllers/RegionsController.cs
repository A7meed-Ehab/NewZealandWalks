using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewZealandWalks.Api.Data;
using NewZealandWalks.Api.Models.Domain;
using NewZealandWalks.Api.Models.DTO;
using System.Globalization;

namespace NewZealandWalks.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public RegionsController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        [Route("List")]
        public IActionResult GetAllRegions()
        {
            var Regions = _dbContext.Regions.ToList();
            var regionsDto = new List<RegionDTO>();
            foreach (var obj in Regions)
            {
                regionsDto.Add(new RegionDTO() {
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
        public IActionResult GetById([FromRoute] Guid id)
        {
            var region = _dbContext.Regions.FirstOrDefault(r => r.Id == id);
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
        [Route("Create",Name ="CreateRegion")]
        public IActionResult Create([FromBody]AddRegionRequestDTO regionDTO)
        {
            if(regionDTO is null)
            {
                return BadRequest();
            }
            Region regoin = new Region
            {
                Name = regionDTO.Name,
                Code = regionDTO.Code,
                RegionImageUrl = regionDTO.RegionImageUrl
            };
            _dbContext.Add(regoin);
            _dbContext.SaveChanges();
            var regionDto = new RegionDTO
            {
                Id = regoin.Id,
                Name = regionDTO.Name,
                Code = regionDTO.Code,
                RegionImageUrl = regionDTO.RegionImageUrl
            };
            return CreatedAtAction(nameof(GetById),new {id= regionDto.Id }, regionDTO);
        }
        [HttpPut]
        [Route("Update/{id:guid}",Name ="UpadateRegion")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDTO updateRegionDto)
        {     
            var regionDomainModel = _dbContext.Regions.FirstOrDefault(r => r.Id == id);
                if(regionDomainModel is null)
                { return NotFound(); }
                regionDomainModel.Name = updateRegionDto.Name;
                regionDomainModel.Code = updateRegionDto.Code;
                regionDomainModel.RegionImageUrl = updateRegionDto.RegionImageUrl;
                _dbContext.SaveChanges();
                var regoinDto = new RegionDTO
                {
                    Id = regionDomainModel.Id,
                    Name = regionDomainModel.Name,
                    Code = regionDomainModel.Code,
                    RegionImageUrl = regionDomainModel.RegionImageUrl
                };
                return Ok(regoinDto);       
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var regionDomainModel=_dbContext.Regions.FirstOrDefault(r=>r.Id == id);
            if( regionDomainModel is null)
                { return NotFound(); }
            _dbContext.Regions.Remove(regionDomainModel);
            _dbContext.SaveChanges();
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
