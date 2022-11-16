using NLayerProjectForJwt.Core.Dtos;
using SharedLibrary;
using System.Threading.Tasks;

namespace NLayerProjectForJwt.Core.Services
{
    public interface IUserService
    {
        Task<Response<UserAppDto>> CreateUserAsync(CreateUserDto createUserDto);

        Task<Response<UserAppDto>> GetUserByNameAsync(string userName);
    }
}
