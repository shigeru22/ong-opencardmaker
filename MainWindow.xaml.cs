﻿using OpenCardMaker.Windows;
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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool godMode; // will be put in config file later
        string ongekiPath;
        string configPath;

        public MainWindow()
        {
            InitializeComponent();
            godMode = true;
        }

        void OpenWindow(object sender, EventArgs e)
        {
            if (godMode == true)
            {
                var window = new GodMain(ongekiPath, configPath);
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
            ongekiPath = OngekiLocationText.Text;

            if (ongekiPath != "") OngekiLocation.Foreground = Brushes.Green;
            else OngekiLocation.Foreground = Brushes.Red;
        }

        void btnConfigLocationClick(object sender, RoutedEventArgs e)
        {
            ConfigLocationText.Text = FrontEndOperations.Browse();
            configPath = ConfigLocationText.Text;

            if (configPath != "") ConfigLocation.Foreground = Brushes.Green;
            else ConfigLocation.Foreground = Brushes.Red;
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
