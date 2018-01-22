using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ContentAuthorizator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();

            var domain = configuration.GetValue<string>("API_DOMAIN") ?? "*";
            var port = configuration.GetValue<string>("API_PORT") ?? "5000";

            var web = WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>()
                .UseUrls("http://" + domain + ":" + port)
                .Build();
            web.Run();
        }
    }
}
