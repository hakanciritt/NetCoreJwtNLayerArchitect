using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace MiniApp2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InvoicesController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetInvoices()
        {
            var userName = HttpContext.User.Identity?.Name;
            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            return Ok($" invoices işlemleri =>  user name : {userName}  |  userId : {userId}");
        }
    }
}
