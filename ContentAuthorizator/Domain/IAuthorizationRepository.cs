using System.Net;

namespace ContentAuthorizator.Domain
{
    public interface IAuthorizationRepository
    {
        IAuthorization Retrieve(IPAddress ipAdress);
        void Store(IAuthorization authorization);
        void Remove(IAuthorization authorization);
    }
}
