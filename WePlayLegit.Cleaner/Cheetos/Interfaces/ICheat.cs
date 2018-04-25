namespace WePlayLegit.Cleaner.Cheetos.Interfaces
{
    public interface ICheat
    {
        /// <summary>
        /// Gets the name of the cheat.
        /// </summary>
        string Name
        {
            get;
        }

        /// <summary>
        /// Detects whether this cheat is on this computer or not.
        /// </summary>
        bool Detect();

        /// <summary>
        /// Removes this cheat from the computer.
        /// </summary>
        void Clean();
    }
}
