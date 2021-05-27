using OpenCardMaker.Operations.AssetsTools;
using System.Drawing;
using System.Windows;

namespace OpenCardMaker.Dialogs
{
    /// <summary>
    /// Interaction logic for CardDetails.xaml
    /// </summary>
    public partial class CardDetails : Window
    {
        public CardDetails(string cardId, CardData target)
        {
            InitializeComponent();

            CardIDBox.Text = cardId;
            CardNameBox.Text = target.Name.str;
            CardAttributeBox.Text = target.Attribute;
            CardSkillBox.Text = target.SkillID.str;
            if (target.LicenseID.id == 0) CopyrightBlock.Text = string.Empty;
            else CopyrightBlock.Text = target.LicenseID.str;
        }

        public CardDetails(string cardId, CardData target, Bitmap image)
        {
            InitializeComponent();

            CardIDBox.Text = cardId;
            CardNameBox.Text = target.Name.str;
            CardAttributeBox.Text = target.Attribute;
            CardSkillBox.Text = target.SkillID.str;

            CardImage.Source = BitmapExtractor.BitmapToImageSource(image);

            if (target.LicenseID.id == 0) CopyrightBlock.Text = string.Empty;
            else CopyrightBlock.Text = target.LicenseID.str;
        }

        public void btnCloseClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
