using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace ContentAuthorizator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new Configuration();

            var web = WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>()
                .UseUrls(config.URL)
                .Build();
            web.Run();
        }
    }
}
