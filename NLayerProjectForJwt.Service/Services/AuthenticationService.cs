using NLayerProjectForJwt.Core.Dtos;
using NLayerProjectForJwt.Core.Services;
using SharedLibrary;
using SharedLibrary.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NLayerProjectForJwt.Core.Configuration;
using NLayerProjectForJwt.Core.Entities;
using NLayerProjectForJwt.Core.Repositories;
using NLayerProjectForJwt.Core.UnitOfWork;

namespace NLayerProjectForJwt.Service.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly List<Client> _client;
        private readonly ITokenService _tokenService;
        private readonly UserManager<UserApp> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<UserRefreshToken> _userRefreshToken;

        public AuthenticationService(IOptions<List<Client>> client,
            ITokenService tokenService,
            UserManager<UserApp> userManager,
            IUnitOfWork unitOfWork,
            IGenericRepository<UserRefreshToken> userRefreshToken)
        {
            _client = client.Value;
            _tokenService = tokenService;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _userRefreshToken = userRefreshToken;
        }
        public async Task<Response<TokenDto>> CreateTokenAsync(LoginDto loginDto)
        {
            if (loginDto == null) throw new ArgumentNullException(nameof(loginDto));

            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) return Response<TokenDto>.Fail("Email veya şifre yanlış", 400, true);

            if (!(await _userManager.CheckPasswordAsync(user, loginDto.Password)))
            {
                return Response<TokenDto>.Fail("Email veya şifre yanlış", 400, true);
            }
            
            var token = _tokenService.CreateToken(user);

            var userRefreshToken = await _userRefreshToken.Where(c => c.UserId == user.Id).SingleOrDefaultAsync();

            if (userRefreshToken == null)
            {
                await _userRefreshToken.AddAsync(new UserRefreshToken()
                { UserId = user.Id, Code = token.RefreshToken, Expiration = token.RefreshTokenExpiration });

            }
            else
            {
                userRefreshToken.Code = token.RefreshToken;
                userRefreshToken.Expiration = token.RefreshTokenExpiration;
            }

            await _unitOfWork.CommitAsync();
            return Response<TokenDto>.Success(token, 200);
        }

        public  Response<ClientTokenDto> CreateTokenByClient(ClientLoginDto loginDto)
        {
            var client = _client.SingleOrDefault(c => c.Id == loginDto.ClientId && c.Secret == loginDto.ClientSecret);
            if (client == null) return Response<ClientTokenDto>.Fail("ClientId veya client secret bulunamadı", 404,true);

            var token = _tokenService.CreateTokenByClient(client);

            return Response<ClientTokenDto>.Success(token, 200);
        }

        public async Task<Response<TokenDto>> CreateTokenByRefreshTokenAsync(string refreshToken)
        {

            var isTokenExists = await _userRefreshToken.Where(c => c.Code == refreshToken).SingleOrDefaultAsync();
            if(isTokenExists==null) return Response<TokenDto>.Fail("Refresh token bulunamadı",404,true);

            var user = await _userManager.FindByIdAsync(isTokenExists.UserId);
            if(user == null) return Response<TokenDto>.Fail("User Id bulunamadı",404,true);

            var tokenDto = _tokenService.CreateToken(user);

            isTokenExists.Code = tokenDto.RefreshToken;
            isTokenExists.Expiration = tokenDto.RefreshTokenExpiration;

            await _unitOfWork.CommitAsync();
            return Response<TokenDto>.Success(tokenDto, 200);
        }

        public async  Task<Response<NoDataDto>> RevokeRefreshToken(string refreshToken)
        {
            var refresh = await _userRefreshToken.Where(c => c.Code == refreshToken).SingleOrDefaultAsync();
            if(refresh == null) return Response<NoDataDto>.Fail("token bulunamadı",404,true);

            _userRefreshToken.Remove(refresh);
            await _unitOfWork.CommitAsync();
            return Response<NoDataDto>.Success(200);
        }
    }
}
