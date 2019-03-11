using System;
namespace Ooievaar.Common
{
    public class ReleaseDTO : IDisposable
    {
        public ReleaseDTO(string pVersion, string pResponsible, string pClient, string pRegressive)
        {
            Version = pVersion;
            Responsible = pResponsible;
            Client = pClient;
            Regressive = pRegressive;
        }

        public string Version { get; set; }
        public string Responsible { get; set; }
        public string Client { get; set; }
        public string Regressive { get; set; }

        public override string ToString()
        {
            return string.Format($"{Version} {Responsible} {Client} {Regressive}");
        }

        public void Dispose() { }
    }
}
