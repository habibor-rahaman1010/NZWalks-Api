using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.DomainEntities;
using NZWalks.API.Dtos.WalksDto;
using NZWalks.API.ServicesInterface;
using NZWalks.API.Utilities;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IWalkManagementService _walkManagementService;
        private readonly IMapper _mapper;
        private readonly IApplicationTime _applicationTime;

        public WalksController(IWalkManagementService walkManagementService,
            IApplicationTime applicationTime,
            IMapper mapper)
        {
            _walkManagementService = walkManagementService;
            _applicationTime = applicationTime;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<WalkDto>> CreatWalkAsync([FromBody] WalkAddRequestDto request)
        {
            if (request == null)
            {
                return BadRequest(new { Message = "The requets dto is null"});
            }
            var walk = _mapper.Map<Walk>(request);
            walk.Id = Guid.NewGuid();
            walk.CreatedDate = _applicationTime.GetCurrentTime();
            await _walkManagementService.AddWalkAsync(walk);

            var walkDto = _mapper.Map<WalkDto>(walk);
            return Ok(walkDto);
        }

        [HttpGet]
        public async Task<ActionResult<ViewWalkDto>> GetWalkListAsync(int pageIndex = 1, int pageSize = 5, string? search = null)
        {
            var (Items, CurrentPage, TotalPages, TotalItems) = await _walkManagementService.GetWalksAsync(pageIndex, pageSize, search);
            if (Items == null)
            {
                return NotFound(new { Message = "No have walks in the database!" });
            };

            var viewWalkDto = _mapper.Map<IList<ViewWalkDto>>(Items);
            return Ok(viewWalkDto);
        }
    }
}
