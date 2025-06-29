using NZWalks.API.Dtos.AuthenticationDto;
using System.Security.Claims;

namespace NZWalks.API.RepositoriesInterface
{
    public interface ITokenRepository
    {

        public Task<string> CreateJwtTokenAsync(ApplicationUser user, List<string> roles);
        public Task<string> GenerateRefreshTokenAsync();

        public Task<string> GenerateAndSaveRefreshTokenAsync(ApplicationUser user);
        public Task<ApplicationUser> ValidateRefreshTokenAsync(Guid userId, string refreshToken);
        public Task<UserLoginResponseDto> CreateResponseTokenAsync(ApplicationUser user);
        public Task<UserLoginResponseDto> RefreshTokenAsync(RefreshTokenRequestDto request);
    }
}
