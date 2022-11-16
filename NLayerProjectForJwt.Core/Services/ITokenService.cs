using NLayerProjectForJwt.Core.Configuration;
using NLayerProjectForJwt.Core.Dtos;
using NLayerProjectForJwt.Core.Entities;

namespace NLayerProjectForJwt.Core.Services
{
    public interface ITokenService
    {
        TokenDto CreateToken(UserApp userApp);

        ClientTokenDto CreateTokenByClient(Client client);
    }
}
