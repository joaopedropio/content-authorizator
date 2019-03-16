using Microsoft.Extensions.Configuration;

namespace ContentAuthorizator
{
    class Configuration
    {
        public string Port { get; private set; }
        public string ElasticSearchURL { get; private set; }
        public string Domain { get; private set; }
        public string URL { get; private set; }

        public Configuration() : this(new ConfigurationBuilder().AddEnvironmentVariables().Build()) { }

        public Configuration(IConfigurationRoot configuration)
        {
            this.ElasticSearchURL = configuration.GetValue<string>("ELASTIC_SEARCH_URL") ?? "http://localhost:9200/";
            this.Domain = configuration.GetValue<string>("API_DOMAIN") ?? "*";
            this.Port = configuration.GetValue<string>("API_PORT") ?? "5000";
            this.URL = string.Format($"http://{this.Domain}:{this.Port}");
        }
    } 
}