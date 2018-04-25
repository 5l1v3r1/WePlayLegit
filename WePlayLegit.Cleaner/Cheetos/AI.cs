namespace WePlayLegit.Cleaner.Cheetos
{
    using System;
    using System.IO;
    using System.Linq;

    using WePlayLegit.Cleaner.Cheetos.Interfaces;

    public class AI : ICheat
    {
        /// <summary>
        /// Gets the name of the cheat.
        /// </summary>
        public string Name
        {
            get
            {
                return this.GetType().Name;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AI"/> class.
        /// </summary>
        public AI()
        {
            // AI.
        }

        /// <summary>
        /// Detects whether this cheat is on this computer or not.
        /// </summary>
        /// <returns></returns>
        public bool Detect()
        {
            DirectoryInfo Temp = new DirectoryInfo(Path.GetTempPath());

            if (Temp.Exists)
            {
                var Directories = Temp.GetDirectories("normal?");

                if (Directories.Length > 0)
                {
                    if (Directories.Any(Directory => Directory.Name.EndsWith("2")))
                    {
                        return true;
                    }
                }

                var Files = Temp.GetFiles("*.exe");

                if (Files.Length > 0)
                {
                    if (Files.Any(File => File.Name.StartsWith("360desk")))
                    {
                        return true;
                    }
                }
            }

            DirectoryInfo AppData = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));

            if (AppData.Exists)
            {
                var Directories = AppData.GetDirectories();

                if (Directories.Length > 0)
                {
                    if (Directories.Any(Directory => Directory.Name.StartsWith("360desk")))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Removes this cheat from the computer.
        /// </summary>
        public void Clean()
        {
            DirectoryInfo Temp = new DirectoryInfo(Path.GetTempPath());

            if (Temp.Exists)
            {
                var Directories = Temp.GetDirectories("normal?");

                if (Directories.Length > 0)
                {
                    if (Directories.Any(Directory => Directory.Name.EndsWith("2")))
                    {
                        Directory.Delete(Path.Combine(Temp.FullName, "normal2"), true);
                    }
                }

                var Files = Temp.GetFiles("*.exe");

                if (Files.Length > 0)
                {
                    if (Files.Any(File => File.Name.StartsWith("360desk")))
                    {
                        File.Delete(Path.Combine(Temp.FullName, "360desk.exe"));
                    }
                }
            }

            DirectoryInfo AppData = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));

            if (AppData.Exists)
            {
                var Directories = AppData.GetDirectories();

                if (Directories.Length > 0)
                {
                    if (Directories.Any(Directory => Directory.Name.StartsWith("360desk")))
                    {
                        Directory.Delete(Path.Combine(AppData.FullName, "360desk"), true);
                    }
                }
            }
        }
    }
}
