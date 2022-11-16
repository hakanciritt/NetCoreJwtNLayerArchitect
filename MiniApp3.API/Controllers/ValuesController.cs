using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        [HttpGet]
        public async Task<IActionResult> GetStock()
        {
            var userName = HttpContext.User.Identity?.Name;
            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            return Ok($" values controller :  user name : {userName}  |  userId : {userId}");
        }
    }
}
