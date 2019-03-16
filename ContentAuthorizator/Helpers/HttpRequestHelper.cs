﻿using Microsoft.AspNetCore.Http;
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
            var ip = request.Headers.FirstOrDefault(h => h.Key == "X-Forwarded-For").Value.ToString();
            
            for (int i = 0; i < request.Headers.Count; i++)
            {
                var heads = request.Headers;
                Console.WriteLine(heads.Keys.ElementAt(i) + " = " + heads.Values.ElementAt(i).ToString());
            }
            Console.WriteLine("Remote IP => " + ip);
            return ip;
        }

        private static string GetAuthorizatonHeader(HttpRequest request)
        {
            return request.Headers.FirstOrDefault(h => h.Key == "Authorization").Value.ToString();
        }
    }
}
