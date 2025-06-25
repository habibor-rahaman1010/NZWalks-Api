namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class DifficultiesController : ApiBaseController
    {
        private readonly IDifficultyManagementService _difficultyManagementService;
        private readonly IApplicationTime _applicationTime;
        private readonly IMapper _mapper;

        public DifficultiesController(IDifficultyManagementService difficultyManagementService,
            IApplicationTime applicationTime,
            IMapper mapper)
        {
            _difficultyManagementService = difficultyManagementService;
            _applicationTime = applicationTime;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IList<DifficultyDto>>> GetDifficultyListAsync([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 5, [FromQuery] string? search = null)
        {
            var (Items, CurrentPage, TotalPages, TotalItems) = await _difficultyManagementService.GetDifficultiesAsync(pageIndex, pageSize, search);
            var result = _mapper.Map<List<DifficultyDto>>(Items);

            return Ok(new
            {
                TotalItems,
                CurrentPage,
                TotalPages,
                Items = result
            });
        }

        [HttpGet, Route("{id:guid}")]
        public async Task<ActionResult<DifficultyDto>> DiffcultyGetByIdAsync([FromRoute] Guid id)
        {
            var difficulty = await _difficultyManagementService.GetByIdDifficultyAsync(id);
            if (difficulty == null)
            {
                return Ok(new {Message = "The difficultry was not found!"});
            }
            var result = _mapper.Map<DifficultyDto>(difficulty);
            return Ok(result);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult<DifficultyDto>> CreateDifficultyAsync([FromBody] DiffcultyAddRequestDto request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            var difficulty = _mapper.Map<Difficulty>(request);
            difficulty.Id = Guid.NewGuid();
            difficulty.CreatedDate = _applicationTime.GetCurrentTime();
            await _difficultyManagementService.AddDifficultyAsync(difficulty);
            var result = _mapper.Map<DifficultyDto>(difficulty);
            return CreatedAtAction(nameof(DiffcultyGetByIdAsync), new { Id = difficulty.Id }, result);
        }

        [HttpPut, Route("{id:guid}")]
        [ValidateModel]
        public async Task<ActionResult<DifficultyDto>> EditDifficultyAsync(Guid id, DifficultyUpdateRequestDto request)
        {
            if(request == null)
            {
                return BadRequest();
            }

            var dificulty = await _difficultyManagementService.GetByIdDifficultyAsync(id);
            if (dificulty == null)
            {
                return Ok(new { Message = "The difficulty was not found!" });
            }

            var updateDifficulty = _mapper.Map(request, dificulty);
            updateDifficulty.ModifiedDate = _applicationTime.GetCurrentTime();
            await _difficultyManagementService.UpdateDifficultyAsync(updateDifficulty);
            var result = _mapper.Map<DifficultyDto>(updateDifficulty);
            return Ok(result);
        }

        [HttpDelete, Route("{id:guid}")]
        public async Task<IActionResult> DifficultyDeleteAsync(Guid id)
        {
            var difficulty = await _difficultyManagementService.GetByIdDifficultyAsync(id);
            if(difficulty == null)
            {
                return Ok(new { Message = "The difficulty was not found!" });
            }

            await _difficultyManagementService.DeleteDifficultyAsync(difficulty);
            return Ok(new {
                Message = "The difficulty hae been deleted!",
                Item = difficulty
            });
        }
    }
}
