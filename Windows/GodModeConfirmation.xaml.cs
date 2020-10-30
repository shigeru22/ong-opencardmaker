using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OpenCardMaker.Windows
{
    /// <summary>
    /// Interaction logic for GodModeConfirmation.xaml
    /// </summary>
    public partial class GodModeConfirmation : Window
    {
        int progress;

        public GodModeConfirmation()
        {
            InitializeComponent();
            progress = 0;
        }

        void btnCancel(object sender, RoutedEventArgs e)
        {
            Close();
        }

        void onLoad(object sender, RoutedEventArgs e)
        {
            var window = GetWindow(this);
            window.KeyDown += GodModeListener;
        }

        void GodModeListener(object sender, KeyEventArgs e)
        {
            if (InputProcessor(e.Key)) progress++;
            else progress = 0;

            if (progress == 11)
            {
                DialogResult = true;
                Close();
            }
        }

        bool InputProcessor(Key key)
        {
            if ((progress == 0 || progress == 1) && key == Key.Up) return true;
            else if ((progress == 2 || progress == 3) && key == Key.Down) return true;
            else if ((progress == 4 || progress == 6) && key == Key.Left) return true;
            else if ((progress == 5 || progress == 7) && key == Key.Right) return true;
            else if (progress == 8 && key == Key.B) return true;
            else if (progress == 9 && key == Key.A) return true;
            else if (progress == 10 && key == Key.Enter) return true;
            else return false;
        }
    }
}
