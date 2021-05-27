using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;
using OpenCardMaker.Operations.About;
using OpenCardMaker.Operations.Exceptions;

namespace OpenCardMaker.Dialogs
{
    /// <summary>
    /// Interaction logic for AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : Window
    {
        public AboutWindow()
        {
            InitializeComponent();

            DependenciesBox.Text = DependenciesText();
        }

        string DependenciesText()
        {
            StringBuilder builder = new StringBuilder();

            int len = Dependencies.list.Length;
            if (len != Dependencies.license.Length) throw new InvalidDataException("List and license aren't the same length.");

            for(int i = 0; i < len; i++)
            {
                builder.AppendLine(Dependencies.list[i]);
                builder.AppendLine(Dependencies.license[i]);

                if (i < len - 1) builder.AppendLine();
            }

            return builder.ToString();
        }

        public void BtnOKClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public void BtnGithubClick(object sender, RoutedEventArgs e)
        {
            string uri = "https://github.com/shigeru22/ong-opencardmaker/";
            Process.Start(new ProcessStartInfo(uri));
        }
    }
}
