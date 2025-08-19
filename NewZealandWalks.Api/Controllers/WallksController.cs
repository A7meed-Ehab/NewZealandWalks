using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewZealandWalks.Api.CustomActionFilters;
using NewZealandWalks.Api.Models.Domain;
using NewZealandWalks.Api.Models.DTO;
using NewZealandWalks.Api.Repositories;

namespace NewZealandWalks.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WallksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWalkRepository _walkRepository;

        public WallksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this._mapper = mapper;
            this._walkRepository = walkRepository;
        }
        [HttpPost]
        [ValidateModel]

        public async Task<IActionResult> Create([FromBody] AddWalkRequestDTO addWalkRequestDTO)
        {
            
                var walkDomainModel = _mapper.Map<Walk>(addWalkRequestDTO);
                await _walkRepository.CreateAsync(walkDomainModel);
                var walkDto = _mapper.Map<WalkDto>(walkDomainModel);
                return Ok(walkDto);
           
                return BadRequest();
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery]string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortby, [FromQuery] bool? isAscending, [FromQuery] int pageNumber=1, [FromQuery] int pageSize = 1000)
        {
            var walksDomain = await _walkRepository.GetAllAsync(filterOn, filterQuery,sortby,isAscending??true, pageNumber, pageSize);
            return Ok(_mapper.Map<List<WalkDto>>(walksDomain));

        }
        [HttpGet]
        [Route("{Id:guid}")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            var walkDomainModel = await _walkRepository.GetByIdAsync(Id);
            if (walkDomainModel is null)
            {
                return NotFound();
            }
            var walkDto = _mapper.Map<WalkDto>(walkDomainModel);
            return Ok(walkDto);
        }
        [HttpPut]
        [ValidateModel]

        public async Task<IActionResult> Update(Guid? Id, [FromBody] updateWalkDto? updateWalkDto)
        {
        
                var domainModel = await _walkRepository.UpdateAsync(Id, updateWalkDto);
                var updatedDomainModel = _mapper.Map<WalkDto>(domainModel);
                return Ok(updatedDomainModel);
     
               
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid? Id)
        {
            if (Id == null || Id == Guid.Empty)
            {
                return NotFound();
            }
            var deletedDomainModel = await _walkRepository.DeleteAsync(Id);
            var modelDto = _mapper.Map<WalkDto>(deletedDomainModel);
            return Ok(modelDto);
        }
    }
}
