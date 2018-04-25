namespace WePlayLegit.Cleaner.Cheetos
{
    using WePlayLegit.Cleaner.Cheetos.Interfaces;

    public class CnCheat : ICheat
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
        /// Initializes a new instance of the <see cref="CnCheat"/> class.
        /// </summary>
        public CnCheat()
        {
            // CnCheat.
        }

        /// <summary>
        /// Detects whether this cheat is on this computer or not.
        /// </summary>
        /// <returns></returns>
        public bool Detect()
        {
            return false;
        }

        /// <summary>
        /// Removes this cheat from the computer.
        /// </summary>
        public void Clean()
        {

        }
    }
}
