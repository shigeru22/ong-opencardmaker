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
    /// Interaction logic for CustomDialog.xaml
    /// </summary>
    public partial class CustomDialog : Window
    {
        public CustomDialog(string title, string dialogText)
        {
            InitializeComponent();

            Title = title;
            DialogText.Text = dialogText;
        }

        private void btnOKClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
