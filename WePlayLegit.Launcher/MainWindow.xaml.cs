namespace PubgTest.StyleLauncher
{
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Input;

    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Gets the Xenforo Rest API client.
        /// </summary>
        public XenforoApi Api
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a value indicating whether this instance is logged in.
        /// </summary>
        public bool IsLoggedIn
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the user's username.
        /// </summary>
        public string Username
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the user's password.
        /// </summary>
        public string Password
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            InitializeApi();
        }

        /// <summary>
        /// Initializes the API Client.
        /// </summary>
        public void InitializeApi()
        {
            this.Api = new XenforoApi();
        }

        /// <summary>
        /// Called when the login button has been clicked.
        /// </summary>
        /// <param name="Sender">The sender.</param>
        /// <param name="Args">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void OnLoginClicked(object Sender, RoutedEventArgs Args)
        {
            if (this.IsLoggedIn)
            {
                return;
            }

            this.Api.Authenticate(this.UsernameField.Text, this.PasswordField.Password);

            if (this.Api.IsAuthenticated)
            {
                this.IsLoggedIn = true;

                var Popup = new PopupPubg("\n\nYou have successfully logged in!", "Ok")
                {
                    Owner = this
                };

                var Result = Popup.ShowDialog();
            }
            else
            {
                var Popup = new PopupPubg("\n\nYour credentials are incorrect, please make sure you are registered.")
                {
                    Owner = this
                };

                this.Hide();

                var Result = Popup.ShowDialog();

                this.Show();
            }
        }

        /// <summary>
        /// Called when the register button has been clicked.
        /// </summary>
        /// <param name="Sender">The sender.</param>
        /// <param name="Args">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void OnRegisterClicked(object Sender, RoutedEventArgs Args)
        {
            Process.Start("https://www.weplaylegit.com/register");
        }

        /// <summary>
        /// Called when the discord button has been clicked.
        /// </summary>
        /// <param name="Sender">The sender.</param>
        /// <param name="Args">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void OnDiscordClicked(object Sender, RoutedEventArgs Args)
        {
            Process.Start("https://discord.gg/mxzAz62");
        }

        /// <summary>
        /// Called when the rocketr button has been clicked.
        /// </summary>
        /// <param name="Sender">The sender.</param>
        /// <param name="Args">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void OnRocketrClicked(object Sender, RoutedEventArgs Args)
        {
            Process.Start("https://rocketr.net/sellers/Ak33m");
        }

        /// <summary>
        /// Called when the main window is being dragged.
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="Args"></param>
        private void OnMainMouseDown(object Sender, MouseButtonEventArgs Args)
        {
            if (Args.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}
