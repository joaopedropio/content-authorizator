using Newtonsoft.Json;
using System.Net;

namespace ContentAuthorizator.Helpers
{
    public class JsonError : Json
    {
        public JsonError(object error, HttpStatusCode statusCode)
            : base(JsonConvert.SerializeObject(error), statusCode)
        {

        }
    }
}
