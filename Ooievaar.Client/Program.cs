using System;
using Microsoft.Extensions.Configuration;

namespace Ooievaar.Client
{

    public static class Program
    {
        public static void Main()
        {
            IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("config.json")
            .Build();

            OoievaarClient ooievaarClient = new OoievaarClient(config);
            ooievaarClient.Start();
        }
    }
}
