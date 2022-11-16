using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NLayerProjectForJwt.Core.Configuration;
using NLayerProjectForJwt.Core.Dtos;
using NLayerProjectForJwt.Core.Entities;
using NLayerProjectForJwt.Core.Services;
using SharedLibrary.Configuration;
using SharedLibrary.Services;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace NLayerProjectForJwt.Service.Services
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<UserApp> _userManager;
        private readonly CustomTokenOptions _tokenOptions;


        public TokenService(UserManager<UserApp> userManager, IOptions<CustomTokenOptions> tokenOptions)
        {
            _userManager = userManager;
            _tokenOptions = tokenOptions.Value;
        }
        public TokenDto CreateToken(UserApp userApp)
        {
            var accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            var refreshTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.RefreshTokenExpiration);
            var securityKey = SignService.GetSymmetricSecurityKey(_tokenOptions.SecurityKey);
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer: _tokenOptions.Issuer,
                expires: accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: GetClaims(userApp, _tokenOptions.Audiences),
                signingCredentials: credentials
            );
            var handler = new JwtSecurityTokenHandler();
            var token = handler.WriteToken(securityToken);
            var tokenDto = new TokenDto()
            {
                AccessToken = token,
                RefreshToken = CreateRefreshToken(),
                AccessTokenExpiration = accessTokenExpiration,
                RefreshTokenExpiration = refreshTokenExpiration
            };
            return tokenDto;
        }

        public ClientTokenDto CreateTokenByClient(Client client)
        {
            var accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            var securityKey = SignService.GetSymmetricSecurityKey(_tokenOptions.SecurityKey);
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer: _tokenOptions.Issuer,
                expires: accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: GetClaimsByClient(client),
                signingCredentials: credentials
            );
            var handler = new JwtSecurityTokenHandler();
            var token = handler.WriteToken(securityToken);
            var tokenDto = new ClientTokenDto()
            {
                AccessToken = token,
                AccessTokenExpiration = accessTokenExpiration,
            };
            return tokenDto;

        }

        private IEnumerable<Claim> GetClaims(UserApp userApp, List<string> audiences)
        {
            var userList = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,userApp.Id),
                new Claim(JwtRegisteredClaimNames.Email,userApp.Email),
                new Claim(ClaimTypes.Name,userApp.UserName)
            };
            userList.AddRange(audiences.Select(c => new Claim(JwtRegisteredClaimNames.Aud, c)));
            return userList;
        }

        private IEnumerable<Claim> GetClaimsByClient(Client client)
        {
            var claims = new List<Claim>();
            claims.AddRange(client.Audiences.Select(c => new Claim(JwtRegisteredClaimNames.Aud, c)));
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, client.Id.ToString()));

            return claims;
        }
        private string CreateRefreshToken()
        {
            var numberByte = new byte[32];
            using var random = RandomNumberGenerator.Create();
            random.GetBytes(numberByte);
            return Convert.ToBase64String(numberByte);
        }
    }
}
