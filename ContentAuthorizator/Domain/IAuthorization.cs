namespace ContentAuthorizator.Domain
{
    public interface IAuthorization
    {
        string Token { get; set; }
        string IpAdress { get; set; }
        string Username { get; set; }
    }
}
