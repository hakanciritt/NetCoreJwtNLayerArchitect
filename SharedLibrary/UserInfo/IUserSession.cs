using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.UserInfo
{
    public interface IUserSession
    {
        Guid? UserId { get; }
        Guid GetUserId { get; }
    }
}
