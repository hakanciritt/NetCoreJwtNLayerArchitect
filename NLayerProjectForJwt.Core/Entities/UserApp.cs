using Microsoft.AspNetCore.Identity;

namespace NLayerProjectForJwt.Core.Entities
{
    public class UserApp : IdentityUser
    {
        public string City { get; set; }
    }
}
