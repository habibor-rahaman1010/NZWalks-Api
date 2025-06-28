using NZWalks.API.Dtos.AuthenticationDto;

namespace NZWalks.API.Controllers.Account
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ApiBaseController
    {
        private readonly ApplicationUserManager _applicationUserManager;
        private readonly IApplicationTime _applicationTime;
        private readonly IMapper _mapper;

        public AuthenticationController(ApplicationUserManager applicationUserManager,
            IApplicationTime applicationTime,
            IMapper mapper)
        {
            _applicationUserManager = applicationUserManager;
            _applicationTime = applicationTime;
            _mapper = mapper;
        }


        [HttpPost, Route("Register")]
        [ValidateModel]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequestDto request)
        {
            if (request == null)
            {
                return BadRequest(new { Message = "The requets dto is null" });
            }

            var applicationUser = new ApplicationUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.Email,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Address = request.Address,
            };

            applicationUser.Id = Guid.NewGuid();
            applicationUser.CreatedDate = _applicationTime.GetCurrentTime();

            var result = await _applicationUserManager.CreateAsync(applicationUser, request.Password);

            if (result.Succeeded)
            {
                var identityResult = await _applicationUserManager.AddToRoleAsync(applicationUser, "Read");
                if (identityResult.Succeeded)
                {
                    return Ok(applicationUser);
                }
            }

            return BadRequest(new { Message = "Somthing went wrong, please try again!" });
        }
    }
}
