﻿namespace NZWalks.API.Dtos.AuthenticationDto
{
    public class UserLoginResponseDto
    {
        public required string Email { get; set; }
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
    }
}
