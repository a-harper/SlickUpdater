using System;
using System.IO;
using SevenZip;

namespace SlickAutoUpdate {
    internal static class Unzippy {
        public static void Extract(string fileName, string directory) {
            SevenZipExtractor.SetLibraryPath("7z.dll");
            try {
                SevenZipExtractor extractor = new SevenZipExtractor(fileName);
                extractor.Extracting += extr_Extracting;
                extractor.FileExtractionStarted += extr_FileExtractionStarted;
                extractor.ExtractionFinished += extr_ExtractionFinished;
                extractor.ExtractArchive(directory);
            } 
            catch (IOException e) {
                
            }
        }

        private static void extr_Extracting(object sender, EventArgs e) {

        }

        private static void extr_FileExtractionStarted(object sender, FileInfoEventArgs e) {
        }

        private static void extr_ExtractionFinished(object sender, EventArgs e) {
            
        }
        /*
        static private void pluginMove(string modPath) {
            string modPluginDir = modPath + "\\plugin";
            if (Directory.Exists(modPluginDir)) {
                string ts3PluginDir = regcheck.ts3RegCheck() + "\\plugins";
                string[] files = Directory.GetFiles(modPluginDir);
                foreach (string file in files) {
                    string filename = Path.GetFileName(file);
                    try { File.Copy(file, ts3PluginDir + "\\" + filename); } catch (IOException) { };
                }
         * */

        private static void DirectoryCopy(string sourceDirName, string destDirName) {
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            // Check if source directory exists and return if it doesn't
            if (!dir.Exists) {
                return;
            }

            //List subdirs
            DirectoryInfo[] dirs = dir.GetDirectories();

            // Check if dest directory exists and create it if it doesn't
            if (!Directory.Exists(destDirName)) {
                Directory.CreateDirectory(destDirName);
            }
            
            // Get all files in directory and move each to destination.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files) {
                string tempPath = Path.Combine(destDirName, file.Name);
                file.CopyTo(tempPath, true);
            }

            // Recursively (is that the right word?) copy subdirectories and files.
            foreach (DirectoryInfo subdir in dirs) {
                string temppath = Path.Combine(destDirName, subdir.Name);
                DirectoryCopy(subdir.FullName, temppath);
            }
        }
    }
}
