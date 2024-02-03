using BudgetTracking.Data.Configurations;
using BudgetTracking.Data.Dto;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Security.Cryptography;
using BudgetTracking.Core.Entities;
using BudgetTracking.Data.Constants;
using Microsoft.Extensions.Options;

namespace BudgetTracking.Service.Services.Token.Commands.CreateToken
{
    public class CreateTokenCommandHandler : IRequestHandler<CreateTokenCommand, Data.Dto.TokenDto>
    {
        private readonly TokenOptionConfigurations _tokenConfigs;

        public CreateTokenCommandHandler(IOptions<TokenOptionConfigurations> tokenConfigs)
        {
            _tokenConfigs = tokenConfigs.Value;
        }

        public async Task<Data.Dto.TokenDto> Handle(CreateTokenCommand request, CancellationToken cancellationToken)
        {
            var accessTokenExpiration = DateTime.Now.AddMinutes(_tokenConfigs.AccessTokenExpiration);
            var refreshTokenExpiration = DateTime.Now.AddMinutes(_tokenConfigs.RefreshTokenExpiration);

            var securityKey = Data.Helpers.SignTokenHelper.GetSymmetricSecurityKey(_tokenConfigs.SecurityKey);

            SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityToken token = new(
                notBefore: DateTime.Now,
                expires: accessTokenExpiration,
                signingCredentials: credentials,
                claims: SetUserClaims(request.User),
                issuer: _tokenConfigs.Issuer
                );


            JwtSecurityTokenHandler tokenHandler = new();

            var createdToken = tokenHandler.WriteToken(token);

            TokenDto tokenDto = new()
            {
                AccessToken = createdToken,
                AccessTokenExpiration = accessTokenExpiration,
                RefreshToken = CreateRefreshToken(),
                RefreshTokenExpiration = refreshTokenExpiration,
            };

            return await Task.FromResult(tokenDto);
        }

        private List<Claim> SetUserClaims(BudgetTracking.Core.Entities.User user)
        {
            List<Claim> claims = new()
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),// kullanıcı id'si bir name identifier'dır
                new(ClaimTypes.Email, user.Email),// kullanıcı emaili
                new(JwtClaimConstants.UserState, user.UserState.ToString()),// kullanıcı durumu
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())// token id'si
            };

            return claims;
        }

        private static string CreateRefreshToken()
        {
            var bytes = new byte[32];
            using var random = RandomNumberGenerator.Create();

            random.GetBytes(bytes);

            return Convert.ToBase64String(bytes);
        }
    }
}
