using NZWalks.API.Dtos.AuthenticationDto;

namespace NZWalks.API.RepositoriesInterface
{
    public interface ITokenRepository
    {
        public Task<string> CreateJwtTokenAsync(ApplicationUser user, List<string> roles);
        public Task<string> GenerateAndSaveRefreshTokenAsync(ApplicationUser user);
        public Task<UserLoginResponseDto> RefreshTokenAsync(RefreshTokenRequestDto request);
    }
}