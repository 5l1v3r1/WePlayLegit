namespace WePlayLegit.Cleaner
{
    using System.Collections.Generic;

    using WePlayLegit.Cleaner.Cheetos;
    using WePlayLegit.Cleaner.Cheetos.Interfaces;

    public class Cheats
    {
        /// <summary>
        /// Gets the handlable cheats.
        /// </summary>
        public List<ICheat> Cheetos
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Cheats"/> class.
        /// </summary>
        public Cheats()
        {
            this.Cheetos = new List<ICheat>(10);
            this.Load();
        }

        /// <summary>
        /// Loads this instance.
        /// </summary>
        public void Load()
        {
            this.Cheetos.Add(new PSSA());
            this.Cheetos.Add(new AI());
            this.Cheetos.Add(new CnCheat());

            // ...
        }

        /// <summary>
        /// Detects if this computers contains cheats
        /// and files that have stayed after the removal.
        /// </summary>
        public List<ICheat> Detect()
        {
            var Cheats = new List<ICheat>(this.Cheetos.Count);

            foreach (var Cheat in this.Cheetos)
            {
                var Detected = Cheat.Detect();

                if (Detected)
                {
                    Cheats.Add(Cheat);
                }
            }

            return Cheats;
        }
    }
}
