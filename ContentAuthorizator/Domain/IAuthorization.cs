using Microsoft.Extensions.Primitives;
using System.Net;

namespace ContentAuthorizator.Domain
{
    public interface IAuthorization
    {
        StringValues Token { get; set; }
        IPAddress IPAdress { get; set; }
    }
}
