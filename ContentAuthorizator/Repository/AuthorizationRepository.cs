using ContentAuthorizator.Domain;
using System.Collections.Generic;
using System.Linq;

namespace ContentAuthorizator.Repository
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

        public IAuthorization RetrieveByIpAddress(string ipAdress)
        {
            return auths.FirstOrDefault(a => a.IpAdress == ipAdress);
        }

        public IAuthorization RetrieveByUsername(string username)
        {
            return auths.FirstOrDefault(a => a.Username == username);
        }

        public void Store(IAuthorization authorization)
        {
            var storedAuth = RetrieveByUsername(authorization.Username);
            if (storedAuth == null)
            {
                auths.Add(authorization);
            }
            else
            {
                var index = auths.IndexOf(storedAuth);
                auths[index].IpAdress = authorization.IpAdress;
                auths[index].Token = authorization.Token;
            }
        }
    }
}
