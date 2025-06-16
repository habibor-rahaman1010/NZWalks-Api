using Microsoft.AspNetCore.Mvc;
using NZWalks.API.DomainEntities;
using NZWalks.API.Dtos.RegionDto;
using NZWalks.API.ServicesInterface;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionManagementService _regionManagementService;

        public RegionsController(IRegionManagementService regionManagementService)
        {
            _regionManagementService = regionManagementService;
        }


        [HttpPost]
        public async Task<ActionResult<Region>> CreateRegion(RegionAddDto request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            var region = new Region
            {
                Id = Guid.NewGuid(),
                Code = request.Code,
                Name = request.Name,
                RegionImageUrl = request.RegionImageUrl,
            };
            await _regionManagementService.AddRegionAsync(region);
            return Ok(region);
        }
    }
}
