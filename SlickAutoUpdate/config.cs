using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace SlickAutoUpdate
{
    public class Repos
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Server { get; set; }
        public string JoinText { get; set; }
        public string Subreddit { get; set; }
    }

    public class Versionfile
    {
        public string Version { get; set; }
        public string Download { get; set; }
        public List<Repos> Repos { get; set; }
    }

    public class Settings
    {
        public static string RepoUrl => ConfigurationManager.AppSettings["RepoUrl"];
        public static string LocalVersionFile => Directory.GetCurrentDirectory() + "\\" + "localversion";
    }
}
