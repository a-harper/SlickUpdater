using System;
using SevenZip;

namespace SlickAutoUpdate {
    internal static class Unzippy {
        public static void Extract(string fileName, string directory) {
            SevenZipBase.SetLibraryPath("7z.dll");
                var extractor = new SevenZipExtractor(fileName);
                extractor.Extracting += extr_Extracting;
                extractor.FileExtractionStarted += extr_FileExtractionStarted;
                extractor.ExtractionFinished += extr_ExtractionFinished;
                extractor.ExtractArchive(directory);
        }

        private static void extr_Extracting(object sender, EventArgs e) {

        }

        private static void extr_FileExtractionStarted(object sender, FileInfoEventArgs e) {
        }

        private static void extr_ExtractionFinished(object sender, EventArgs e) {
            
        }
    }
}
