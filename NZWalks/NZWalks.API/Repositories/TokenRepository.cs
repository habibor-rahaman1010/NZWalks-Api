using NZWalks.API.Dtos.AuthenticationDto;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace NZWalks.API.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IApplicationTime _applicationTime;
        private readonly ApplicationUserManager _applicationUserManager;
        public TokenRepository(IConfiguration configuration, IApplicationTime applicationTime, ApplicationUserManager applicationUserManager)
        {
            _applicationUserManager = applicationUserManager;
            _configuration = configuration;
            _applicationTime = applicationTime;
        }

        public async Task<string> CreateJwtTokenAsync(ApplicationUser user, List<string> roles)
        {
            return await Task<string>.Run(() => {

                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Email, user.Email));

                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                        issuer: _configuration["Jwt:Issuer"],
                        audience: _configuration["Jwt:Audience"],
                        claims: claims,
                        expires: _applicationTime.GetUtcNowTime().AddMinutes(1),
                        signingCredentials: credentials
                    );

                return new JwtSecurityTokenHandler().WriteToken(token);
            });
        }


        public async Task<string> GenerateAndSaveRefreshTokenAsync(ApplicationUser user)
        {
            var refreshToken = await GenerateRefreshTokenAsync();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = _applicationTime.GetUtcNowTime().AddDays(15);
            await _applicationUserManager.UpdateAsync(user);
            return refreshToken;
        }
       

        public async Task<UserLoginResponseDto> RefreshTokenAsync(RefreshTokenRequestDto request)
        {
            var user = await ValidateRefreshTokenAsync(request.UserId, request.RefreshToken);
            if (user == null) return null;
            return await CreateResponseTokenAsync(user);
        }

        private async Task<string> GenerateRefreshTokenAsync()
        {
            return await Task<string>.Run(() =>
            {
                var randomNumber = new byte[32];
                using var rng = RandomNumberGenerator.Create();
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            });
        }

        private async Task<ApplicationUser> ValidateRefreshTokenAsync(Guid userId, string refreshToken)
        {
            var user = await _applicationUserManager.FindByIdAsync(userId.ToString());
            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= _applicationTime.GetCurrentTime())
            {
                return null;
            }
            return user;
        }

        private async Task<UserLoginResponseDto> CreateResponseTokenAsync(ApplicationUser user)
        {
            var roles = await _applicationUserManager.GetRolesAsync(user);
            return new UserLoginResponseDto
            {
                AccessToken = await CreateJwtTokenAsync(user, roles.ToList()),
                RefreshToken = await GenerateAndSaveRefreshTokenAsync(user),
                Email = user.Email
            };
        }
    }
}
