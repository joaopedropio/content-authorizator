using ContentAuthorizator.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Linq;
using System.Net;

namespace ContentAuthorizator.Helpers
{
    public static class HttpRequestHelper
    {
        public static Domain.Authorization GetAuthorization(HttpRequest request)
        {
            return new Domain.Authorization(GetIPAdress(request), GetAuthorizatonHeader(request));
        }

        public static bool IsRequestValid(HttpRequest request)
        {
            var token = GetAuthorizatonHeader(request);
            var ip = GetIPAdress(request);

            return IsIPAdressValid(ip) && IsTokenValid(token);
        }

        private static bool IsIPAdressValid(IPAddress ipAdress)
        {
            return ipAdress != null;
        }

        private static bool IsTokenValid(StringValues token)
        {
            return token.Count != 0;
        }

        private static IPAddress GetIPAdress(HttpRequest request)
        {
            return request.HttpContext.Connection.RemoteIpAddress;
        }

        private static StringValues GetAuthorizatonHeader(HttpRequest request)
        {
            return request.Headers.FirstOrDefault(h => h.Key == "Authorization").Value;
        }
    }
}
