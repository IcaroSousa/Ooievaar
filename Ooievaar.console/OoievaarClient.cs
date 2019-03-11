using System;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using Ooievaar.Common;

namespace Ooievaar.Client
{
    public class OoievaarClient
    {
        private readonly RedisQueue _redisQueue = new RedisQueue("queue", "Ooievaar");

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
            consoleSpinner.Dalay = TimeSpan.FromSeconds(1);
            while (true) 
            {
                consoleSpinner.Turn("Aguardando release", pAnimationSequence: 1);

                string releaseItem = string.Empty; //_redisQueue.Dequeue();

                if (!string.IsNullOrWhiteSpace(releaseItem))
                {
                    ReleaseDTO release = JsonConvert.DeserializeObject<ReleaseDTO>(releaseItem);
                    ExecuteReleaseRobot(release);
                }

            }
        }

    }
}
