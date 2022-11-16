using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayerProjectForJwt.Core.Dtos;
using NLayerProjectForJwt.Core.Services;
using SharedLibrary.Exceptions;

namespace NLayerProjectForJwt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDto userDto)
        {
            var result = await _userService.CreateUserAsync(userDto);
            
            return ActionResultInstance(result);
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> GetUser()
        {
            var name = HttpContext.User.Identity?.Name;
            var result = await _userService.GetUserByNameAsync(name);
            return ActionResultInstance(result);
        }
    }
}
