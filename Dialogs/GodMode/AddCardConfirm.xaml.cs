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

namespace OpenCardMaker.Dialogs.GodMode
{
    /// <summary>
    /// Interaction logic for AddCardInfo.xaml
    /// </summary>
    public partial class AddCardConfirm : Window
    {
        public AddCardConfirm(CardData card)
        {
            InitializeComponent();

            CardIDBox.Text = card.dataName.Substring(4);
            CardNameBox.Text = card.CharaID.str;
            CardAttributeBox.Text = card.Attribute;
            CardSkillBox.Text = card.SkillID.str;
            if (card.LicenseID.id == 0) CopyrightBlock.Text = string.Empty;
            else CopyrightBlock.Text = card.LicenseID.str;
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
