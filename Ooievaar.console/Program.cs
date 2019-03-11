using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Ooievaar.Client
{

    public static class Program
    {
        public static void Main()
        {
            var enumerator = Environment.GetEnvironmentVariables().GetEnumerator();
            while (enumerator.MoveNext())
            {
                Console.WriteLine($"{enumerator.Key,5}:{enumerator.Value,100}");
            }

            OoievaarClient ooievaarClient = new OoievaarClient();
            ooievaarClient.Start();
        }
    }
}
