using OpenCardMaker.Windows;
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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool godMode = true; // will be put in config file later

        public MainWindow()
        {
            InitializeComponent();
            godMode = false;
        }

        void btnOngekiLocationClick(object sender, RoutedEventArgs e)
        {
            OngekiLocationText.Text = Operations.Browse();
        }

        void btnConfigLocationClick(object sender, RoutedEventArgs e)
        {
            ConfigLocationText.Text = Operations.Browse();
        }

        void btnGodModeClick(object sender, RoutedEventArgs e)
        {
            if (godMode)
            {
                GodModeToggle();
                return;
            }

            var target = new GodModeConfirmation();
            
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
