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

namespace OpenCardMaker
{
    public enum OngekiType
    {
        Plus = 2,
        Summer = 3
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool godMode; // will be put in config file later
        OngekiType type;
        PathSettings settings;

        public MainWindow()
        {
            settings = Preferences.LoadPreferences();
            type = settings.type;

            InitializeComponent();
            godMode = true;
            OngekiLocationText.Text = settings.ongeki;
            ConfigLocationText.Text = settings.config;
            // type?
        }

        void OpenWindow(object sender, EventArgs e)
        {
            Preferences.SavePreferences(settings);

            if (godMode == true)
            {
                var window = new GodMain(settings.ongeki, settings.config);
                window.Show();
            }
            else
            {
                // var window = new Window();
                // window.Show();
            }
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

        void btnGodModeClick(object sender, RoutedEventArgs e)
        {
            if (godMode)
            {
                GodModeToggle();
                return;
            }

            var target = new Dialogs.GodModeConfirmation();
            
            bool? status = target.ShowDialog();
            switch(status)
            {
                case true: GodModeToggle(); break;
                default: break;
            }
        }

        public void GodModeToggle()
        {
            if (godMode)
            {
                godMode = false;
                GodModeBtn.Content = "Enable God Mode";
            }
            else
            {
                godMode = true;
                GodModeBtn.Content = "Disable God Mode";
            }
        }
    }
}
