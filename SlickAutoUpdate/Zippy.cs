using System;
using SevenZip;

namespace SlickAutoUpdate {
    internal static class Zippy {
        public static void Compress(string source, string outputFileName) {
            if (source == null || outputFileName == null) return;
            SevenZipBase.SetLibraryPath("7z.dll");
            var cmp = new SevenZipCompressor();
            cmp.Compressing += cmp_Compressing;
            cmp.FileCompressionStarted += cmp_FileCompressionStarted;
            cmp.CompressionFinished += cmp_CompressionFinished;
            cmp.ArchiveFormat = (OutArchiveFormat)Enum.Parse(typeof(OutArchiveFormat), "SevenZip");
            cmp.CompressFiles(outputFileName, source);
        }
        private static void cmp_Compressing(object sender, ProgressEventArgs e) {

        }
        private static void cmp_FileCompressionStarted(object sender, FileNameEventArgs e) {

        }
        private static void cmp_CompressionFinished(object sender, EventArgs e) {
        }
    }
}
