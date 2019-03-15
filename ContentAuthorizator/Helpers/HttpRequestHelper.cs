using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
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
            var result = request.Headers.FirstOrDefault(h => h.Key == "UserIP").Value.ToString();
            PrintHeaders(request);
            return result;
        }

        public static void PrintHeaders(HttpRequest httpRequest)
        {
            for (int i = 0; i < httpRequest.Headers.Count; i++)
            {
                var heads = httpRequest.Headers;
                Console.WriteLine(heads.Keys.ElementAt(i) + " = " + heads.Values.ElementAt(i).ToString());
            }
        } 

        private static string GetAuthorizatonHeader(HttpRequest request)
        {
            return request.Headers.FirstOrDefault(h => h.Key == "Authorization").Value.ToString();
        }
    }
}
