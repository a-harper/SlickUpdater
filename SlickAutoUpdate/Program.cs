using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading;
using log4net;
using Newtonsoft.Json;

namespace SlickAutoUpdate
{
    internal class Program
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static string[] _localversion;
        private static readonly WebClient Client = new WebClient();
        private static Versionfile _slickversion;

        private static void Main(string[] args)
        {
            string rawSlickJson = null;
            log4net.Config.XmlConfigurator.Configure();
            if (File.Exists(Settings.LocalVersionFile))
            {
                Log.Info("Reading current local version from " + Settings.LocalVersionFile);
                _localversion = File.ReadAllLines(Settings.LocalVersionFile);
                Log.Info("Localversion: " + _localversion);
            }
            else
            {
                Log.Info("Current version not found in " + Settings.LocalVersionFile);
                Console.WriteLine("Did not find local version at " + Settings.LocalVersionFile);
            }

            try
            {
                Log.Info("Attempting to connect to " + Settings.RepoUrl);
                rawSlickJson = Reader.WebRead(Settings.RepoUrl);
                _slickversion = JsonConvert.DeserializeObject<Versionfile>(rawSlickJson);
            }
            catch
            {
                Log.Warn("Couldn't connect to the repo! Aborting.");
                Console.WriteLine("Unable to connect to repo. Please check your internet connection and try again.");
                Console.WriteLine("Press any key to exit");
                Console.ReadLine();
            }


            if (rawSlickJson == null) return;
            if (_slickversion.Version == _localversion[0])
            {
                Log.Info("Local and remote versions are both " + _slickversion.Version);
                Console.WriteLine("Current version is up to date. Press any key to exit.");
                Console.ReadLine();
            }
            else
            {
                Log.Info("Newer version available, downloading");
                Console.WriteLine("Repo version is newer than local version, updating...");
                try
                {
                    Client.DownloadFile(_slickversion.Download, "newSlickVersion.zip");
                }
                catch (Exception e)
                {
                    Log.Warn("Issue downloading new version! " + e.Message);
                    Console.WriteLine("Error downloading new version, check log for details.");
                    Console.WriteLine("Press any key to exit");
                    Console.ReadLine();
                    return;
                }
                

                Log.Info("New version downloaded OK");
                Console.WriteLine("New version downloaded OK");
                Log.Info("Unzipping new version to current directory");
                try
                {
                    Unzippy.Extract("newSlickVersion.zip", Directory.GetCurrentDirectory());
                    Log.Info("File extracted successfully");
                }
                catch (Exception e)
                {
                    Log.Warn("Couldn't extract file! " + e.Message);
                    Console.WriteLine("Problem extracting file, please see log for details");
                    Console.WriteLine("Press any key to exit");
                    Console.ReadLine();
                    return;
                }
                Log.Info("Deleting download zip");
                try
                {
                    File.Delete("newSlickVersion.zip");
                    Log.Info("File deleted successfully");
                }
                catch(Exception e)
                {
                    Log.Warn("Couldn't delete zip file. " + e.Message);
                    Console.WriteLine("Couldn't delete file newSlickVersion.zip, please delete it manually");
                }
                
                Console.WriteLine("Slick Updater now version " + _slickversion.Version);
                Console.WriteLine("Press any key to exit");
                Console.ReadLine();

            }
            Log.Info("Exiting");
        }
    }
}
