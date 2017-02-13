using System;
using System.IO;
using System.Net;
using System.Threading;
using Newtonsoft.Json;

namespace SlickAutoUpdate
{
    internal class Program
    {
        private static string[] _localversion;
        private static readonly WebClient Client = new WebClient();
        private static Versionfile _slickversion;

        private static void Main(string[] args)
        {
            var rawSlickJson = Reader.WebRead(Settings.RepoUrl);
            _slickversion = JsonConvert.DeserializeObject<Versionfile>(rawSlickJson);

            if (File.Exists(Directory.GetCurrentDirectory() + "\\" + "localversion"))
            {
                _localversion = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\" + "localversion");
                //Console.WriteLine("Found localversion");
            }
            else { 
                Console.WriteLine("Did not find local version at " + Directory.GetCurrentDirectory() + "\\" + "localversion"); 
            }
            
            try
            {
            } catch (WebException) {
                Console.WriteLine("ERROR: Could not locate web server");
            }
            if (rawSlickJson != null)
            {


                if (_slickversion.Version == _localversion[0])
                {
                    Console.WriteLine("All is up to date so why are you launching this again?");
                }

                if (_slickversion.Version!= _localversion[0])
                {
                    Console.WriteLine("Found a new version of slick updater downloading now...");
                    Client.DownloadFile(_slickversion.Download, "newSlickVersion.zip");
                    Console.WriteLine("Ok downloaded the new version just have to extract it now");
                    Unzippy.Extract("newSlickVersion.zip", Directory.GetCurrentDirectory());
                    File.Delete("newSlickVersion.zip");
                    Console.WriteLine("Ok its all updated killing this thread in 3 secs");
                }
            }
            Thread.Sleep(3000);
        }
    }
}
