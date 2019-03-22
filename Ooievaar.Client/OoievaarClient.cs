using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Ooievaar.Common;
using Ooievaar.Common.Classes;
using Ooievaar.Common.Interfaces;

namespace Ooievaar.Client
{
    public class OoievaarClient
    {
        private static IConfiguration _config;
        private static IRedisQueue _redisQueue;

        public OoievaarClient(IConfiguration pConfig) 
        {
            _config = pConfig;
            _redisQueue = new RedisQueue(_config);
        }

        private void ExecuteReleaseRobot(ReleaseDTO pReleaseItem) 
        { 
            using(Process process = new Process()) 
            {
                process.StartInfo.FileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ReleaseBot.exe");
                process.StartInfo.Arguments = pReleaseItem.ToString();

                if (process == null)
                    throw new Exception("Falha ao iniciar ReleaseBot!");

                Console.WriteLine($"Executando ReleaseBot com os paramêtros -> {pReleaseItem.ToString()}");

                process.Start();
                process.WaitForExit();
            }
        }

        public void Start()
        {
            ConsoleSpinner consoleSpinner = new ConsoleSpinner();
            consoleSpinner.Delay = TimeSpan.FromSeconds(1);
            while (true) 
            {
                consoleSpinner.Turn("Aguardando release", pAnimationSequence: 1);

                string releaseItem = _redisQueue.Dequeue();

                if (!string.IsNullOrWhiteSpace(releaseItem))
                {
                    ReleaseDTO release = JsonConvert.DeserializeObject<ReleaseDTO>(releaseItem);
                    ExecuteReleaseRobot(release);
                }

            }
        }

    }
}
