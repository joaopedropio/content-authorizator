using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace ContentAuthorizator.Helpers
{
    public class Json : ContentResult
    {
        public Json(HttpStatusCode statusCode)
        {
            this.StatusCode = statusCode.GetHashCode();
            this.ContentType = "application/json";
        }
        public Json(Domain.IAuthorization auth, HttpStatusCode statusCode)
            : this(JsonConvert.SerializeObject(auth), statusCode)
        { }

        public Json(string content, HttpStatusCode statusCode)
        {
            this.Content = content;
            this.ContentType = "application/json";
            this.StatusCode = statusCode.GetHashCode();
        }
    }
}
