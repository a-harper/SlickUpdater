using System;
using System.IO;
using System.Net;

namespace SlickAutoUpdate
{
    internal class Reader
    {
        public static string WebRead(string url)
        {
            var client = new WebClient();
            var stream = client.OpenRead(url);
            if (stream != null)
            {
                var reader = new StreamReader(stream);
                var content = reader.ReadToEnd();
                return content;
            }
            else
            {
                throw new ArgumentNullException(url);
            }
        }
    }
}
