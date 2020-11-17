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
    public partial class AddCardInfo : Window
    {
        // unused since images are in assets bundle format
        // (unityengine needed for extracting those)
        public AddCardInfo(CardData card, string path)
        {
            CardIDBox.Text = card.dataName;
            CardNameBox.Text = card.CharaID.str;
            CardAttributeBox.Text = card.Attribute;
            CardSkillBox.Text = card.SkillID.str;
            if (card.LicenseID.id == 0) CopyrightBlock.Text = string.Empty;
            else CopyrightBlock.Text = card.LicenseID.str;

            InitializeComponent();
        }
    }
}
