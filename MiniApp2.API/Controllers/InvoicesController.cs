using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.UserInfo;
using System.Threading.Tasks;

namespace MiniApp2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InvoicesController : ControllerBase
    {
        private readonly IUserSession _userSession;

        public InvoicesController(IUserSession userSession)
        {
            _userSession = userSession;
        }

        [HttpGet]
        public async Task<IActionResult> GetInvoices()
        {
            var userName = HttpContext.User.Identity?.Name;

            return Ok($" invoices işlemleri =>  user name : {userName}  |  userId : {_userSession.GetUserId}");
        }
    }
}
