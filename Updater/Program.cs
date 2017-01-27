using System;
using System.IO;
using System.Net;
using System.Threading;
using System.IO.Compression;

namespace Updater
{
    class Program
    {
        private static bool IsLocked(string parPathAndFile)
        {
            try
            {
                if (File.Exists(parPathAndFile))
                {
                    // ReSharper disable once UnusedVariable
                    using (Stream stream = new FileStream(parPathAndFile, FileMode.Open))
                    {
                    }
                }
                return false;
            }
            catch
            {
                Console.WriteLine("File \"" + parPathAndFile + "\" is stil in use.");
                return true;
            }
        }

        private static readonly string[] Files = new string[]
        {
            "Internal\\ZzukBot.exe",
            "Internal\\DomainManager.dll",
            "Internal\\Fasm.NET.dll",
            "Internal\\Loader.dll"
        };

        static void Main()
        {
            bool canUpdate = false;
            while (!canUpdate)
            {
                Console.Clear();
                canUpdate = true;
                Console.WriteLine("## ZzukBot's Updater ##\n\nChecking if we can start the update (please leave this window open):");
                foreach (string file in Files)
                {
                    if (IsLocked(file))
                        canUpdate = false;
                }
                if (!canUpdate)
                    Console.WriteLine("Rechecking in a second ...");
                Thread.Sleep(1000);
            }

            Console.WriteLine("Ready to update ...");
            Console.WriteLine("Reading download url from Updater.ini");

            string downloadPath = "";
            try
            {
                downloadPath = File.ReadAllText("Updater.ini")
                    .Trim();
            }
            catch {
                Console.Write("Updater.ini does not exist. Aborting ");
                Console.ReadLine();
                Environment.Exit(-1);
            }
            Console.WriteLine("Path is: " + downloadPath);

            var zipTempFilePath = Path.GetTempFileName();
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    webClient.DownloadFile(downloadPath, zipTempFilePath);
                }
            }
            catch
            {
                Console.Write("Downloading from URL caused a crash. Aborting ");
                Console.ReadLine();
                Environment.Exit(-1);
            }

            Console.WriteLine("Download completed ...");
            string updateFolder = Path.GetTempPath() + "\\ZzukBot";
            try
            {
                if (Directory.Exists(updateFolder))
                    Directory.Delete(updateFolder, true);
            }
            catch
            {

            }
            ZipFile.ExtractToDirectory(zipTempFilePath, updateFolder);
            File.Delete(zipTempFilePath);

            updateFolder += "\\ZzukBot";

            foreach (string dirPath in Directory.GetDirectories(updateFolder, "*",
                SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(updateFolder, ".\\"));

            foreach (string newPath in Directory.GetFiles(updateFolder, "*.*",
            SearchOption.AllDirectories))
                File.Copy(newPath, newPath.Replace(updateFolder, ".\\"), true);

            Directory.Delete(updateFolder, true);
            Console.Write("Update completed! Press any key to exit and start ZzukBot.cmd");
            Console.ReadLine();





        }
    }
}
