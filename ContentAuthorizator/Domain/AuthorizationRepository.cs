using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace ContentAuthorizator.Domain
{
    public class AuthorizationRepository : IAuthorizationRepository
    {
        private IList<IAuthorization> auths;

        public AuthorizationRepository()
            :this(new List<IAuthorization>())
        { }

        public AuthorizationRepository(IList<IAuthorization> auths)
        {
            this.auths = auths;
        }

        public void Remove(IAuthorization authorization)
        {
            auths.Remove(authorization);
        }

        public IAuthorization Retrieve(IPAddress ipAdress)
        {
            return auths.FirstOrDefault(a => a.IPAdress == ipAdress);
        }

        public void Store(IAuthorization authorization)
        {
            var storedAuth = Retrieve(authorization.IPAdress);
            if (storedAuth == null)
            {
                auths.Add(authorization);
            }
            else
            {
                var index = auths.IndexOf(storedAuth);
                auths[index].Token = authorization.Token;
            }
        }
    }
}
