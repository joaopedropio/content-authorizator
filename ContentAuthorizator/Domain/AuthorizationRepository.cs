using System;
using System.Net;

namespace ContentAuthorizator.Domain
{
    public class AuthorizationRepository : IAuthorizationRepository
    {
        public void Remove(IAuthorization authorization)
        {
            throw new NotImplementedException();
        }

        public IAuthorization Retrieve(IPAddress ipAdress)
        {
            throw new NotImplementedException();
        }

        public void Store(IAuthorization authorization)
        {
            throw new NotImplementedException();
        }
    }
}
