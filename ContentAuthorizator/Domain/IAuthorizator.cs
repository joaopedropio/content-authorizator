namespace ContentAuthorizator.Domain
{
    public interface IAuthorizator
    {
        bool IsAuthorizationValid(IAuthorization auth);
    }
}
