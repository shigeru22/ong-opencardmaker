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
        public MainWindow()
        {
            InitializeComponent();
        }

        public void btnOngekiLocationClick(object sender, RoutedEventArgs e)
        {
            OngekiLocationText.Text = Operations.Browse();
        }

        public void btnConfigLocationClick(object sender, RoutedEventArgs e)
        {
            ConfigLocationText.Text = Operations.Browse();
        }
    }
}
