using OpenCardMaker.Operations;
using OpenCardMaker.Windows;
using OpenCardMaker.Windows.GodMode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OpenCardMaker.Operations.Preferences;

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
                var dialog = new Dialogs.CustomDialog("Error", "No mu3.exe and mu3_Data folder found.");
                dialog.ShowDialog();

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
                var dialog = new Dialogs.CustomDialog("Error", "No related config files found.");
                dialog.ShowDialog();

                ConfigLocation.Foreground = Brushes.Red;
            }
        }
    }
}