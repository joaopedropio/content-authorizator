using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace ContentAuthorizator.Helpers
{
    public static class HttpRequestHelper
    {
        public static bool IsRequestValid(HttpRequest request)
        {
            var token = GetAuthorizatonHeader(request);
            var ip = GetIPAdress(request);

            return IsIPAdressValid(ip) && IsTokenValid(token);
        }

        private static bool IsIPAdressValid(string ipAdress)
        {
            return !string.IsNullOrEmpty(ipAdress);
        }

        private static bool IsTokenValid(string token)
        {
            return !string.IsNullOrEmpty(token);
        }

        public static string GetIPAdress(HttpRequest request)
        {
            // return request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            var result = request.Headers.FirstOrDefault(h => h.Key == "X-Real-IP").Value.ToString();
            Console.WriteLine(result);
            return result;
        }

        private static string GetAuthorizatonHeader(HttpRequest request)
        {
            return request.Headers.FirstOrDefault(h => h.Key == "Authorization").Value.ToString();
        }
    }
}
