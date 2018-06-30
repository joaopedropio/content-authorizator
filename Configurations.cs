using Microsoft.Extensions.Configuration;

namespace ContentAuthorizator
{
   class Configuration
   {
       public string Port { get; set; }
       public string Domain { get; set; }
       public string URL { get; set; }

       public Configuration() 
            : this(new ConfigurationBuilder().AddEnvironmentVariables().Build())
       {
           
       }

       public Configuration(IConfigurationRoot configuration)
       {
            this.Domain = configuration.GetValue<string>("API_DOMAIN") ?? "*";
            this.Port = configuration.GetValue<string>("API_PORT") ?? "5000";
            this.URL = string.Format($"http://{this.Domain}:{this.Port}");
       }
   } 
}