namespace ContentAuthorizator.Domain
{
    public interface IAuthorizator
    {
        bool IsAuthorizationValid(IAuthorization auth);

        IAuthorizationRepository Auths { get; set; }
    }
}
