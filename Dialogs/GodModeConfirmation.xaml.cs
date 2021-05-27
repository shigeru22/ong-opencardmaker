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

namespace OpenCardMaker.Dialogs
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
            bool ret = false;

            switch(progress)
            {
                case 0:
                    if (key.Equals(Key.Up)) ret = true;
                    break;
                case 1:
                    if (key.Equals(Key.Up)) ret = true;
                    break;
                case 2:
                    if (key.Equals(Key.Down)) ret = true;
                    break;
                case 3:
                    if (key.Equals(Key.Down)) ret = true;
                    break;
                case 4:
                    if (key.Equals(Key.Left)) ret = true;
                    break;
                case 5:
                    if (key.Equals(Key.Right)) ret = true;
                    break;
                case 6:
                    if (key.Equals(Key.Left)) ret = true;
                    break;
                case 7:
                    if (key.Equals(Key.Right)) ret = true;
                    break;
                case 8:
                    if (key.Equals(Key.B)) ret = true;
                    break;
                case 9:
                    if (key.Equals(Key.A)) ret = true;
                    break;
                case 10:
                    if (key.Equals(Key.Enter)) ret = true;
                    break;
            }

            return ret;
        }
    }
}
