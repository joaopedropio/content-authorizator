using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Linq;
using System.Net;

namespace ContentAuthorizator.Domain
{
    public class Authorization : IAuthorization
    {
        public StringValues Token { get; set; }
        public IPAddress IPAdress { get; set; }

        public Authorization(IPAddress ipAdress, StringValues token)
        {
            this.IPAdress = ipAdress;
            this.Token = token;
        }

        public override bool Equals(object obj)
        {
            Authorization auth;
            try
            {
                auth = (Authorization)obj;
            }
            catch (Exception)
            {
                return false;
            }

            return this.IPAdress.ToString() == auth.IPAdress.ToString() &&
                   this.Token[0].ToString() == auth.Token[0].ToString();
        }
    }
}
