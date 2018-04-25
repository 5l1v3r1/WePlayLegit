namespace WePlayLegit.Models
{
    using System.Collections.Generic;

    public class JGame
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
        /// Gets the last version.
        /// </summary>
        public string Last
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the versions.
        /// </summary>
        public List<JVersion> Versions
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the last version.
        /// </summary>
        public JVersion GetLastVersion()
        {
            var LastVersion = this.Versions.Find(T => T.Version == Last);

            if (LastVersion != null)
            {
                return LastVersion;
            }

            return null;
        }
    }
}