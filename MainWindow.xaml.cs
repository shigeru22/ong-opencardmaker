using OpenCardMaker.Operations;
using OpenCardMaker.Operations.Preferences;
using OpenCardMaker.Windows;
using System;
using System.Windows;
using System.Windows.Media;

namespace OpenCardMaker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // OngekiType type; // TODO: Check for certain cards unavailable in its executable packages
        PathSettings settings;

        public MainWindow()
        {
            settings = Preferences.LoadPreferences();

            InitializeComponent();
            OngekiLocationText.Text = settings.ongeki;
            ConfigLocationText.Text = settings.config;

            if (FileChecker.OngekiFolderCheck(settings.ongeki)) OngekiLocation.Foreground = Brushes.Green;
            else OngekiLocation.Foreground = Brushes.Red;

            if (FileChecker.ConfigFolderCheck(settings.config)) ConfigLocation.Foreground = Brushes.Green;
            else ConfigLocation.Foreground = Brushes.Red;
        }

        void OpenWindow(object sender, EventArgs e)
        {
            Preferences.SavePreferences(settings);

            var window = new GodMain(settings.ongeki, settings.config);
            window.Show();
        }

        void btnOKClick(object sender, RoutedEventArgs e)
        {
            Closing += OpenWindow;
            Close();
        }

        void btnOngekiLocationClick(object sender, RoutedEventArgs e)
        {
            OngekiLocationText.Text = FrontEndOperations.Browse();
            string temp = OngekiLocationText.Text;

            if (FileChecker.OngekiFolderCheck(temp))
            {
                settings.ongeki = temp;
                OngekiLocation.Foreground = Brushes.Green;
            }
            else
            {
                MessageBox.Show("No mu3.exe and mu_Data folder found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                OngekiLocation.Foreground = Brushes.Red;
            }
        }

        void btnConfigLocationClick(object sender, RoutedEventArgs e)
        {
            ConfigLocationText.Text = FrontEndOperations.Browse();
            string temp = ConfigLocationText.Text;

            if (FileChecker.ConfigFolderCheck(temp))
            {
                settings.config = temp;
                ConfigLocation.Foreground = Brushes.Green;
            }
            else
            {
                MessageBox.Show("No related config files found.\nRun the game once after patching the game with UnityParrot.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                ConfigLocation.Foreground = Brushes.Red;
            }
        }
    }
}