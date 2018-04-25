namespace WePlayLegit
{
    using RestSharp;

    using WePlayLegit.Models;

    public static class CheatApi
    {
        /// <summary>
        /// Gets a value indicating whether this <see cref="CheatApi"/> is initialized.
        /// </summary>
        public static bool Initialized
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the rest client.
        /// </summary>
        public static RestClient Rest
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public static void Initialize()
        {
            if (CheatApi.Initialized)
            {
                return;
            }

            CheatApi.Rest        = new RestClient("http://laichiji123.com");
            CheatApi.Initialized = true;
        }

        /// <summary>
        /// Gets the downloads list.
        /// </summary>
        public static JCheats GetDownloadsList()
        {
            if (!CheatApi.Initialized)
            {
                return null;
            }

            var Request = new RestRequest("/downloads/list", Method.GET);
            var Result  = CheatApi.Rest.Get<JCheats>(Request);

            if (Result.IsSuccessful)
            {
                return Result.Data;
            }

            return null;
        }
    }
}