namespace WePlayLegit.Cleaner.Cheetos
{
    using System;
    using System.IO;
    using System.Linq;

    using WePlayLegit.Cleaner.Cheetos.Interfaces;

    public class PSSA : ICheat
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
        /// Initializes a new instance of the <see cref="PSSA"/> class.
        /// </summary>
        public PSSA()
        {
            // PSSA.
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
                    if (Directories.Any(Directory => Directory.Name.EndsWith("6")))
                    {
                        return true;
                    }
                }

                var Files = Temp.GetFiles("*.exe");

                if (Files.Length > 0)
                {
                    if (Files.Any(File => File.Name.StartsWith("sina")))
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
                    if (Directories.Any(Directory => Directory.Name.StartsWith("sina")))
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
                    if (Directories.Any(Directory => Directory.Name.EndsWith("6")))
                    {
                        Directory.Delete(Path.Combine(Temp.FullName, "normal6"), true);
                    }
                }

                var Files = Temp.GetFiles("*.exe");

                if (Files.Length > 0)
                {
                    if (Files.Any(File => File.Name.StartsWith("sina")))
                    {
                        File.Delete(Path.Combine(Temp.FullName, "sina.exe"));
                    }
                }
            }

            DirectoryInfo AppData = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));

            if (AppData.Exists)
            {
                var Directories = AppData.GetDirectories();

                if (Directories.Length > 0)
                {
                    if (Directories.Any(Directory => Directory.Name.StartsWith("sina")))
                    {
                        Directory.Delete(Path.Combine(AppData.FullName, "sina"), true);
                    }
                }
            }
        }
    }
}
