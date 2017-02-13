using System;
using System.IO;
using System.Net;

namespace SlickAutoUpdate
{
    internal class Reader
    {
        public static string WebRead(string url)
        {
            WebClient client = new WebClient();
            Stream stream = client.OpenRead(url);
            StreamReader reader = new StreamReader(stream);
            String content = reader.ReadToEnd();
            return content;
        }
    }
}
