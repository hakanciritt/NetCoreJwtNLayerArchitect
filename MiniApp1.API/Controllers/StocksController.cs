using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.UserInfo;

namespace MiniApp1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StocksController : ControllerBase
    {
        private readonly IUserSession _userSession;

        public StocksController(IUserSession userSession)
        {
            _userSession = userSession;
        }
        [HttpGet]
        public async Task<IActionResult> GetStock()
        {
            var userName = HttpContext.User.Identity?.Name;

            return Ok($" Stok işlemleri => user name : {userName}  |  userId : {_userSession.GetUserId}");
        }
    }
}
