using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.DomainEntities;
using NZWalks.API.Dtos.RegionsDto;
using NZWalks.API.ServicesInterface;
using NZWalks.API.Utilities;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionManagementService _regionManagementService;
        private readonly IMapper _mapper;
        private readonly IApplicationTime _applicationTime;

        public RegionsController(IRegionManagementService regionManagementService,
            IApplicationTime applicationTime,
            IMapper mapper)
        {
            _regionManagementService = regionManagementService;
            _applicationTime = applicationTime;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IList<RegionDto>>> GetRegionListAsync(int pageIndex = 1, int pageSize = 5, string? search = null)
        {
            var (Items, CurrentPage, TotalPages, TotalItems) = await _regionManagementService.GetRegionsAsync(pageIndex, pageSize, search);

            if (Items == null)
            {
                return NotFound(new { Message = "The region was not found!" });
            }
            if (Items.Count == 0)
            {
                return Ok(new { Message = "The Region List Is Empty!" });
            }
            var regionDto = _mapper.Map<IList<RegionDto>>(Items);
            return Ok(new
            {
                TotalItems,
                CurrentPage,
                TotalPages,
                Items = regionDto
            });
        }

        [HttpGet, Route("{id:guid}")]
        public async Task<ActionResult<RegionDto>> RegionGetByIdAsync([FromRoute] Guid id)
        {
            var region = await _regionManagementService.GetByIdRegion(id);
            if (region == null)
            {
                return NotFound(new { Message = "The region was not found!" });
            }
            var regionDto = _mapper.Map<RegionDto>(region);
            return Ok(regionDto);
        }

        [HttpPost]
        public async Task<ActionResult<RegionDto>> CreateRegionAsync([FromBody] RegionAddDto request)
        {
            if (request == null)
            {
                return BadRequest();
            }
            var region = _mapper.Map<Region>(request);
            region.Id = Guid.NewGuid();
            region.CreatedDate = _applicationTime.GetCurrentTime();
            await _regionManagementService.AddRegionAsync(region);

            var result = _mapper.Map<RegionDto>(region);
            return CreatedAtAction(nameof(RegionGetByIdAsync), new { Id = region.Id }, result);
        }

        [HttpPut, Route("{id:guid}")] 
        public async Task<ActionResult<RegionDto>> RegionUpdateAsync([FromRoute] Guid id, [FromBody] RegionUpdateRequestDto request)
        {
            var region = await _regionManagementService.GetByIdRegion(id);
            if (region == null)
            {
                return NotFound(new { Message = "The region was not found!" });
            }

            var regionUpdate = _mapper.Map(request, region);
            regionUpdate.ModifiedDate = _applicationTime.GetCurrentTime();
            await _regionManagementService.UpdateRegionAsync(regionUpdate);
            var result = _mapper.Map<RegionDto>(regionUpdate);
            return Ok(new { Massege = "Region Update succesfully!", Result = result});
        }

        [HttpDelete, Route("{id:guid}")]
        public async Task<ActionResult<RegionDto>> RegionDeleteAsync([FromRoute] Guid id)
        {
            var region = await _regionManagementService.GetByIdRegion(id);
            if(region == null)
            {
                return NotFound(new { Message = "The region was not found!" });
            }
            var result = _mapper.Map<RegionDto>(region);
            await _regionManagementService.DeleteRegionAsync(region);
            return Ok(new { Message = "Region has succesfully deleted!", Result = result});
        }
    }
}
