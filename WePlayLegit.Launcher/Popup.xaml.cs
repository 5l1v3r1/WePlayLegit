namespace PubgTest.StyleLauncher
{
    using System.Windows;

    /// <summary>
    /// Logique d'interaction pour PopupPubg.xaml
    /// </summary>
    public partial class PopupPubg : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PopupPubg"/> class.
        /// </summary>
        public PopupPubg()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PopupPubg"/> class.
        /// </summary>
        public PopupPubg(string Message) : this()
        {
            if (string.IsNullOrEmpty(Message) == false)
            {
                this.SetMessage(Message);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PopupPubg"/> class.
        /// </summary>
        /// <param name="Message">The message.</param>
        /// <param name="ButtonText">The button text.</param>
        public PopupPubg(string Message, string ButtonText) : this()
        {
            if (string.IsNullOrEmpty(Message) == false)
            {
                this.SetMessage(Message);
            }

            if (string.IsNullOrEmpty(ButtonText) == false)
            {
                this.SetButton(ButtonText);
            }
        }

        /// <summary>
        /// Sets the message.
        /// </summary>
        /// <param name="Message">The message.</param>
        public void SetMessage(string Message)
        {
            this.PopupMessage.Text = Message;
        }

        /// <summary>
        /// Sets the button.
        /// </summary>
        /// <param name="Message">The message.</param>
        public void SetButton(string Message)
        {
            this.PopupButton.Content = Message;
        }

        /// <summary>
        /// Handles the Click event of the Button control.
        /// </summary>
        /// <param name="Sender">The source of the event.</param>
        /// <param name="Args">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Button_Click(object Sender, RoutedEventArgs Args)
        {
            this.Close();
        }
    }
}
