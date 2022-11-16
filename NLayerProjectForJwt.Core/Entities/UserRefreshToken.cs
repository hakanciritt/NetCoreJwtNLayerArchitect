using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NLayerProjectForJwt.Core.Entities
{
    public class UserRefreshToken
    {
         
        public string UserId { get; set; } 
        public string Code { get; set; }
        public DateTime Expiration { get; set; }

    }
}
