using ContentAuthorizator.Domain;

namespace ContentAuthorizator.Repository
{
    public interface IAuthorizationRepository
    {
        IAuthorization RetrieveByIpAddress(string ipAdress);
        IAuthorization RetrieveByUsername(string username);
        void Store(IAuthorization authorization);
        void Remove(IAuthorization authorization);
    }
}
