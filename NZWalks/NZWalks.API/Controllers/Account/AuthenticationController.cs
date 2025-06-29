using NZWalks.API.Dtos.AuthenticationDto;

namespace NZWalks.API.Controllers.Account
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ApiBaseController
    {
        private readonly ApplicationUserManager _applicationUserManager;
        private readonly IApplicationTime _applicationTime;
        private readonly ITokenRepository _tokenRepository;
        private readonly IMapper _mapper;

        public AuthenticationController(ApplicationUserManager applicationUserManager,
            IApplicationTime applicationTime,
            ITokenRepository tokenRepository,
            IMapper mapper)
        {
            _applicationUserManager = applicationUserManager;
            _applicationTime = applicationTime;
            _tokenRepository = tokenRepository;
            _mapper = mapper;
        }


        [HttpPost, Route("Register")]
        [ValidateModel]
        public async Task<IActionResult> RegisterAsync([FromBody] UserRegisterRequestDto request)
        {
            if (request == null)
            {
                return BadRequest(new { Message = "The requets dto is null" });
            }

            var existingByEmail = await _applicationUserManager.FindByEmailAsync(request.Email);
            if (existingByEmail != null)
            {
                return BadRequest(new { Message = "Email is already registered. Please login or use another email." });
            }

            var existingByUserName = await _applicationUserManager.FindByNameAsync(request.Email);
            if (existingByUserName != null)
            {
                return BadRequest(new { Message = "Username is already taken. Please choose another one." });
            }

            var applicationUser = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.Email,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Address = request.Address,
                CreatedDate = _applicationTime.GetCurrentTime()
            };

            var result = await _applicationUserManager.CreateAsync(applicationUser, request.Password);

            if (result.Succeeded)
            {
                var identityResult = await _applicationUserManager.AddToRoleAsync(applicationUser, "Read");
                if (identityResult.Succeeded)
                {
                    return Ok(new { Message = "User registration has been successfully, Login now!" });
                }
            }

            return BadRequest(new { Message = "Somthing went wrong, please try again!" });
        }


        [HttpPost, Route("Login")]
        [ValidateModel]
        public async Task<IActionResult> LoginAsync([FromBody] UserLoginRequestDto request)
        {
            var user = await _applicationUserManager.FindByEmailAsync(request.Email);

            if (user != null)
            {
                var checkPasswordResult = await _applicationUserManager.CheckPasswordAsync(user, request.Password);
                if (checkPasswordResult == true)
                {
                    var roles = await _applicationUserManager.GetRolesAsync(user);
                    if (roles != null)
                    {
                        var token = await _tokenRepository.CreateJwtTokenAsync(user, roles.ToList());
                        return Ok(new
                        {
                            Message = "User logedin successfully!",
                            Email = user.Email,
                            JwtToken = token,
                            RefreshToken = await _tokenRepository.GenerateAndSaveRefreshTokenAsync(user),
                        });
                    }
                }
                else
                {
                    return Unauthorized(new { Message = "Invalid email or password." });
                }
            }

            return Unauthorized(new { Message = "User email and password wrong! try again with new one!" });
        }


        [HttpPost("Refresh-Token")]
        public async Task<ActionResult<UserLoginResponseDto>> RefreshToken(RefreshTokenRequestDto request)
        {
            var result = await _tokenRepository.RefreshTokenAsync(request);
            if (result == null || result.AccessToken == null || result.RefreshToken == null || result.Email == null)
            {
                return Unauthorized("Invalid Refresh Token");
            }
            return Ok(result);
        }
    }
}
