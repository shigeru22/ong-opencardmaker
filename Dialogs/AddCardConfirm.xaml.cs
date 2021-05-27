using OpenCardMaker.Operations.AssetsTools;
using System.Drawing;
using System.Windows;

namespace OpenCardMaker.Dialogs
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

        public AddCardConfirm(CardData card, Bitmap image)
        {
            InitializeComponent();

            CardIDBox.Text = card.dataName.Substring(4);
            CardNameBox.Text = card.CharaID.str;
            CardAttributeBox.Text = card.Attribute;
            CardSkillBox.Text = card.SkillID.str;

            CardImage.Source = BitmapExtractor.BitmapToImageSource(image);

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
