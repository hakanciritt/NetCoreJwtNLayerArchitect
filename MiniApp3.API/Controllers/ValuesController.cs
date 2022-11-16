using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.UserInfo;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MiniApp3.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ValuesController : ControllerBase
    {
        private readonly IUserSession _userSession;

        public ValuesController(IUserSession userSession)
        {
            _userSession = userSession;
        }

        [HttpGet]
        public async Task<IActionResult> GetStock()
        {
            var userName = HttpContext.User.Identity?.Name;

            return Ok($" values controller :  user name : {userName}  |  userId : {_userSession.GetUserId}");
        }
    }
}
