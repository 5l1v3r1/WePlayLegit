namespace PubgTest.StyleLauncher
{
    using System;
    using System.CodeDom;
    using System.Diagnostics;

    using Newtonsoft.Json.Linq;

    using RestSharp;
    using RestSharp.Authenticators;

    public class XenforoApi
    {
        /// <summary>
        /// Gets the rest client.
        /// </summary>
        public RestClient Client
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the access token.
        /// </summary>
        public string AccessToken
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the refresh token.
        /// </summary>
        public string RefreshToken
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the type of the token.
        /// </summary>
        public string TokenType
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the token creation date.
        /// </summary>
        public DateTime TokenCreation
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the duration of the token.
        /// </summary>
        public TimeSpan TokenDuration
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the token expiration date.
        /// </summary>
        public DateTime TokenExpiration
        {
            get
            {
                return this.TokenCreation.Add(this.TokenDuration);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is authenticated.
        /// </summary>
        public bool IsAuthenticated
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a value indicating whether this instance is expired.
        /// </summary>
        public bool IsExpired
        {
            get
            {
                return DateTime.UtcNow >= this.TokenExpiration;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XenforoApi"/> class.
        /// </summary>
        public XenforoApi()
        {
            this.Client = new RestClient("https://www.weplaylegit.com/api/");
        }

        /// <summary>
        /// Authenticates the user using the specified credentials.
        /// </summary>
        /// <param name="Username">The username.</param>
        /// <param name="Password">The password.</param>
        public void Authenticate(string Username, string Password)
        {
            var Request     = new RestRequest("?oauth/token");

            Request.AddParameter("client_id",       "kRCV5T_HzG");
            Request.AddParameter("client_secret",   "R310B9fbNVUmWYV");
            Request.AddParameter("grant_type",      "password");

            Request.AddParameter("username",        Username);
            Request.AddParameter("password",        Password);

            var Response    = this.Client.Post(Request);

            if (Response.IsSuccessful)
            {
                var Json = JObject.Parse(Response.Content);

                if (Json != null && Json.HasValues)
                {
                    if (!Json.ContainsKey("access_token"))
                    {
                        Debug.WriteLine("[*] Json.ContainsKey('access_token') != true at XenforoApi.Authenticate(Username, Password).");
                    }
                    else
                    {
                        this.AccessToken    = Json.GetValue("access_token").ToObject<string>();
                        this.RefreshToken   = Json.GetValue("refresh_token").ToObject<string>();
                        this.TokenType      = Json.GetValue("token_type").ToObject<string>();

                        if (this.TokenType == "Bearer")
                        {
                            this.Client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(this.AccessToken, this.TokenType);
                        }
                        else
                        {
                            this.Client.Authenticator = new OAuth2UriQueryParameterAuthenticator(this.AccessToken);
                        }

                        this.TokenCreation  = DateTime.UtcNow;
                        this.TokenDuration  = TimeSpan.FromSeconds(Json.GetValue("expires_in").ToObject<int>());

                        // Check if authenticated

                        Debug.WriteLine("[*] The token expires in " + this.TokenDuration.TotalMinutes + "minute(s).");
                        Debug.WriteLine("[*] The token is <" + this.AccessToken + ">.");

                        int VisitorId       = this.GetVisitorId();

                        if (VisitorId != -1)
                        {
                            Debug.WriteLine("[*] VisitorId is " + VisitorId + ".");

                            this.IsAuthenticated = true;
                        }
                        else
                        {
                            Debug.WriteLine("[*] VisitorId == -1 at XenforoApi.Authenticate(Username, Password).");
                        }
                    }
                }
                else
                {
                    Debug.WriteLine("[*] Json == null || Json.HasValues != true at XenforoApi.Authenticate(Username, Password).");
                }
            }
            else
            {
                Debug.WriteLine("[*] Response.IsSuccessful != true at XenforoApi.Authenticate(Username, Password).");
                Debug.WriteLine("[*] ErrorCode    = " + Response.StatusCode + ".");
                Debug.WriteLine("[*] ErrorMessage = " + Response.ErrorMessage + ".");
            }
        }

        /// <summary>
        /// Makes the request at the specified url.
        /// </summary>
        public int GetVisitorId()
        {
            var VisitorId   = -1;
            var Request     = new RestRequest();
            var Response    = this.Client.Get(Request);

            if (Response.IsSuccessful)
            {
                var Json    = JObject.Parse(Response.Content);

                if (Json != null && Json.HasValues)
                {
                    if (Json.ContainsKey("system_info"))
                    {
                        var SystemArr  = Json["system_info"];

                        if (SystemArr.Type != JTokenType.Object)
                        {
                            return -1;
                        }

                        var SystemInfo = SystemArr.ToObject<JObject>();

                        if (SystemInfo != null && SystemInfo.HasValues)
                        {
                            if (SystemInfo.ContainsKey("visitor_id"))
                            {
                                VisitorId = SystemInfo.GetValue("visitor_id").ToObject<int>();
                            }
                            else
                            {
                                Debug.WriteLine("[*] Json.ContainsKey('visitor_id') != true at XenforoApi.GetVisitorId().");
                            }
                        }
                        else
                        {
                            Debug.WriteLine("[*] SystemInfo == null || SystemInfo.HasValues != true at XenforoApi.GetVisitorId()."); ;
                        }
                    }
                    else
                    {
                        Debug.WriteLine("[*] Json.ContainsKey('system_info') != true at XenforoApi.GetVisitorId().");
                    }
                }
                else
                {
                    Debug.WriteLine("[*] Json == null || Json.HasValues != true at XenforoApi.GetVisitorId().");
                }
            }
            else
            {
                Debug.WriteLine("[*] Response.IsSuccessful != true at XenforoApi.GetVisitorId().");
                Debug.WriteLine("[*] ErrorCode    = " + Response.StatusCode + ".");
                Debug.WriteLine("[*] ErrorMessage = " + Response.ErrorMessage + ".");
                Debug.WriteLine("[*] ErrorUrl     = " + Response.ResponseUri + ".");
            }

            return VisitorId;
        }
    }
}