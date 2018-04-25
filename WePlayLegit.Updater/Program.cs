namespace WePlayLegit
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Runtime.InteropServices;
    using System.Threading;

    using Newtonsoft.Json.Linq;

    using PSHostsFile;

    public class Program
    {
        [DllImport("kernel32.dll")]
        public static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr Handle, int Command);

        public const int SW_HIDE = 0;
        public const int SW_SHOW = 5;

        /// <summary>
        /// Gets the console handle.
        /// </summary>
        public static IntPtr Handle
        {
            get
            {
                return Program.GetConsoleWindow();
            }
        }

        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        public static void Main()
        {
            Console.Title = "[*] WePlayLegit - Updater";

            Program.SetHosts();

            Console.WriteLine("[*] Setting up..");

            CheatApi.Initialize();

            Console.WriteLine("[*] Getting the latest configuration..");

            var Cheats   = CheatApi.GetDownloadsList();

            Console.WriteLine("[*] Detected " + Cheats.Count + " cheats : ");
            Console.WriteLine();

            foreach (var Cheat in Cheats)
            {
                Console.WriteLine("[*] - " + Cheat.Name + ((Cheat.Name == "normal2") ? " - (AI)" : (Cheat.Name == "normal6") ? " - (PSSA)" : string.Empty));
            }

            Console.WriteLine();
            Console.Write("[*] > ");

            string Selected = Console.ReadLine();

            Console.WriteLine();

            if (Cheats.All(T => T.Name != Selected))
            {
                SetError("Error code 0x01. (Cheat not found)");
            }

            var SelectedCheat = Cheats.Find(T => T.Name == Selected);

            if (SelectedCheat == null)
            {
                SetError("Error code 0x02. (Cheat instance not found)");
            }

            Console.WriteLine("[*] Cheat has been selected and is valid.");

            if (SelectedCheat.Games.All(T => T.Id != "TslLaucher"))
            {
                SetError("Error code 0x03. (Cheat is not valid for PUBG)");
            }

            var GameCheat = SelectedCheat.Games.Find(T => T.Id == "TslLaucher");

            if (string.IsNullOrEmpty(GameCheat.Last))
            {
                SetError("Error code 0x04. (Cheat is not released yet)");
            }

            var CheatVersion = GameCheat.GetLastVersion();

            Console.WriteLine("[*] Found " + GameCheat.Versions.Count + " versions of this cheat.");
            Console.WriteLine("[*] Choosing the most recent one, " + CheatVersion.Version + ".");

            var WPLPath  = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "WePlayLegit/", SelectedCheat.Id, GameCheat.Id);
            var WPLFold  = new DirectoryInfo(WPLPath);
            var Filename = CheatVersion.Version + ".exe";

            Console.WriteLine();
            Console.WriteLine("[*] Searching for a previous installation of this cheat..");

            if (WPLFold.Exists)
            {
                var WPLFiles = WPLFold.GetFiles("*.exe");

                if (WPLFiles.Length > 0)
                {
                    var Executable = WPLFiles.FirstOrDefault(File => File.Name == Filename);

                    if (Executable != null)
                    {
                        Console.WriteLine("[*] The cheat is already on your computer !");
                        Console.WriteLine("[*] Loading it..");

                        Thread.Sleep(1500);

                        Process.Start(Executable.FullName);
                        return;
                    }
                }
            }

            Console.WriteLine("[*] You never ran this cheat before, let me create the folders..");

            WPLFold.Create();

            Console.WriteLine("[*] Downloading the cheat..");

            var Downloader = new WebClient();
            var CheatBuff  = Downloader.DownloadData(Path.Combine(CheatVersion.Path, "TslLogin.exe"));

            File.WriteAllBytes(Path.Combine(WPLPath, Filename), CheatBuff);

            Console.WriteLine("[*] Cheat has been downloaded and saved !");
            Console.WriteLine("[*] Loading it..");

            Process.Start(Path.Combine(WPLPath, Filename));

            Thread.Sleep(1500);
        }

        /// <summary>
        /// Sets the hosts.
        /// </summary>
        public static void SetHosts()
        {
            HostsFile.Set(new HostsFileEntry[]
            {
                new HostsFileEntry("www.laichiji123.com", "163.172.4.181"),
                new HostsFileEntry("laichiji123.com",     "163.172.4.181"),
            });
        }

        /// <summary>
        /// Sets the error.
        /// </summary>
        /// <param name="Message">The message.</param>
        public static void SetError(string Message)
        {
            ShowWindow(Handle, Program.SW_SHOW);
            Console.WriteLine("[*] " + Message);
            Console.ReadKey(false);
            Environment.Exit(0);
        }
    }
}
