using NLayerProjectForJwt.Core.Dtos;
using SharedLibrary;
using SharedLibrary.Dtos;
using System.Threading.Tasks;

namespace NLayerProjectForJwt.Core.Services
{
    public interface IAuthenticationService
    {
        Task<Response<TokenDto>> CreateTokenAsync(LoginDto loginDto);

        Task<Response<TokenDto>> CreateTokenByRefreshTokenAsync(string refreshToken);

        Task<Response<NoDataDto>> RevokeRefreshToken(string refreshToken);

        Response<ClientTokenDto> CreateTokenByClient(ClientLoginDto loginDto);
    }
}
