using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace ContentAuthorizator.Domain
{
    public class Authorization : IAuthorization
    {
        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }

        [JsonProperty(PropertyName = "token")]
        public string Token { get; set; }

        [JsonProperty(PropertyName = "ipadress")]
        public string IpAdress { get; set; }

        [JsonConstructor]
        public Authorization(string username, string ipaddress, string token)
        {
            this.Username = username;
            this.IpAdress = ipaddress;
            this.Token = token;
        }

        public static Authorization Parse(Stream body)
        {
            var json = ParseStream(body);
            return JsonConvert.DeserializeObject<Authorization>(json);
        }

        private static string ParseStream(Stream stream)
        {
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);

                var bytes = memoryStream.GetBuffer();

                return Encoding.UTF8.GetString(bytes);
            }
        }
    }
}
