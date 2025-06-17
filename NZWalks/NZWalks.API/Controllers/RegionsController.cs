using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.DomainEntities;
using NZWalks.API.Dtos.RegionDto;
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
        public async Task<ActionResult<IList<RegionDto>>> GetRegionListAsync(int pageIndex = 1, int pageSize = 5)
        {
            var region = await _regionManagementService.GetRegionsAsync(pageIndex, pageSize);
            if (region.Items == null)
            {
                return NotFound();
            }
            if (region.Items.Count == 0)
            {
                return Ok(new { Message = "The Region List Is Empty!" });
            }
            var regionDto = _mapper.Map<IList<RegionDto>>(region.Items);
            return Ok(new
            {
                region.TotalItems,
                region.CurrentPage,
                region.TotalPages,
                Items = regionDto
            });
        }

        [HttpGet, Route("{id:Guid}")]
        public async Task<ActionResult<RegionDto>> RegionGetById([FromRoute] Guid id)
        {
            var region = await _regionManagementService.GetByIdRegion(id);
            if (region == null)
            {
                return NotFound();
            }
            var regionDto = _mapper.Map<RegionDto>(region);
            return Ok(regionDto);
        }

        [HttpPost]
        public async Task<ActionResult<Region>> CreateRegion([FromBody] RegionAddDto request)
        {
            if (request == null)
            {
                return BadRequest();
            }
            var region = _mapper.Map<Region>(request);
            region.CreatedDate = _applicationTime.GetCurrentTime();
            await _regionManagementService.AddRegionAsync(region);

            var result = _mapper.Map<RegionDto>(region);
            return CreatedAtAction(nameof(RegionGetById), new { Id = region.Id }, result);
        }

        [HttpPut, Route("{id:Guid}")] 
        public async Task<ActionResult<RegionDto>> RegionUpdate([FromRoute] Guid id, [FromBody] RegionUpdateRequestDto request)
        {
            var region = await _regionManagementService.GetByIdRegion(id);
            if (region == null)
            {
                return NotFound();
            }

            var regionUpdate = _mapper.Map(request, region);
            regionUpdate.ModifiedDate = _applicationTime.GetCurrentTime();
            await _regionManagementService.UpdateRegionAsync(regionUpdate);
            var result = _mapper.Map<RegionDto>(regionUpdate);
            return Ok(new { Massege = "Region Update succesfully!", Result = result});
        }

        [HttpDelete, Route("{id:Guid}")]
        public async Task<IActionResult> RegionDelete([FromRoute] Guid id)
        {
            var region = await _regionManagementService.GetByIdRegion(id);
            if(region == null)
            {
                return NotFound();
            }
            await _regionManagementService.DeleteRegionAsync(region);
            return Ok(new { Message = "Region has succesfully deleted!" });
        }
    }
}
