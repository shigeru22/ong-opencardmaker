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
using System.Windows.Shapes;

namespace OpenCardMaker.Dialogs
{
    /// <summary>
    /// Interaction logic for IncreaseCardCap.xaml
    /// </summary>
    public partial class IncreaseCardCap : Window
    {
        public int capNumber { get; private set; }
        readonly int cap, maxCap;

        public IncreaseCardCap(int cap, int maxCap)
        {
            // add max cap check

            capNumber = 10;
            this.cap = cap;
            this.maxCap = maxCap;
            InitializeComponent();

            MainText.Text = $"Increase card level cap to (maximum {this.maxCap}):";
            UpdateText();
            DecreaseCapButton.IsEnabled = false;
        }

        void UpdateText()
        {
            CapNumberText.Text = capNumber.ToString();
            TargetCapText.Text = $"Target cap: {cap} -> {cap + capNumber}";
        }

        public void btnDecreaseCap(object sender, RoutedEventArgs e)
        {
            if (capNumber > 10)
            {
                capNumber -= 10;
                UpdateText();

                if (capNumber == 10) DecreaseCapButton.IsEnabled = false;
                else if (!IncreaseCapButton.IsEnabled && capNumber < maxCap - cap) IncreaseCapButton.IsEnabled = true;
            }
        }

        public void btnIncreaseCap(object sender, RoutedEventArgs e)
        {
            if (capNumber < maxCap - cap)
            {
                capNumber += 10;
                UpdateText();

                if (capNumber == maxCap - cap) IncreaseCapButton.IsEnabled = false;
                else if (!DecreaseCapButton.IsEnabled && capNumber > 10) DecreaseCapButton.IsEnabled = true;
            }
        }

        public void btnOKClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        public void btnCancelClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
