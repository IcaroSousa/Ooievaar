using System;
using System.Text.RegularExpressions;

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

        private string ExtractUserFromEmail(String pEmail)
        {
            string _pattern = @"(\w+\.?\w+)";
            Regex _regex = new Regex(_pattern);
            return _regex.Match(Responsible).Value;
        }

        public string Version { get; set; }
        public string Responsible { get; set; }
        public string Client { get; set; }
        public string Regressive { get; set; }

        public override string ToString()
        {
            return string.Format($"{Version} {ExtractUserFromEmail(Responsible)} {Client} {Regressive}");
        }

        public void Dispose() { }
    }
}
