using System.Collections.Generic;

namespace NLayerProjectForJwt.Core.Configuration
{
    public class Client
    {
        public Client()
        {
            Audiences = new List<string>();
        }
        public string Id { get; set; }

        public string Secret { get; set; }

        public List<string> Audiences { get; set; }
    }
}
