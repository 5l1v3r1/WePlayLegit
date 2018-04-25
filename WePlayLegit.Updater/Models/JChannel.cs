namespace WePlayLegit.Models
{
    using System.Collections.Generic;

    public class JChannel
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        public string Id
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the path.
        /// </summary>
        public string Path
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the games.
        /// </summary>
        public List<JGame> Games
        {
            get;
            private set;
        }
    }
}