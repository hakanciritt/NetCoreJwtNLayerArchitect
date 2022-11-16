using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayerProjectForJwt.Core.Dtos;
using NLayerProjectForJwt.Core.Services;

namespace NLayerProjectForJwt.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateToken(LoginDto loginDto)
        {
            var result = await _authenticationService.CreateTokenAsync(loginDto);

            return ActionResultInstance(result);
        }

        [HttpPost]
        public IActionResult CreateTokenByClient(ClientLoginDto loginDto)
        {

            var result = _authenticationService.CreateTokenByClient(loginDto);

            return ActionResultInstance(result);
        }

        [HttpPost]
        public async Task<IActionResult> RevokeRefreshToken(string refreshToken)
        {
            var result = await _authenticationService.RevokeRefreshToken(refreshToken);

            return ActionResultInstance(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTokenByRefreshToken(string refreshToken)
        {
            var result = await _authenticationService.CreateTokenByRefreshTokenAsync(refreshToken);
            return ActionResultInstance(result);
        }
    }
}
