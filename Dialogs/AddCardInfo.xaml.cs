using System.Windows;

namespace OpenCardMaker.Dialogs
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
