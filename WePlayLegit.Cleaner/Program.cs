namespace WePlayLegit.Cleaner
{
    using System;

    internal class Program
    {
        /// <summary>
        /// Gets a value indicating whether the files need to be removed.
        /// </summary>
        private const bool Clean = true;

        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        private static void Main()
        {
            Console.WriteLine("[*] Setting up the cleaner...");

            var Detector = new Cheats();
            var Cheats   = Detector.Detect();

            foreach (var Cheat in Cheats)
            {
                Console.WriteLine("[*] Detected " + Cheat.Name);
            }

            if (Program.Clean)
            {
                foreach (var Cheat in Cheats)
                {
                    Cheat.Clean();
                }
            }

            if (Cheats.Count == 0)
            {
                Console.WriteLine("[*] Not a single cheat has been detected, GJ.");
            }

            Console.ReadKey(false);
        }
    }
}