using OpenCardMaker.Operations;
using System;
using System.Windows;

namespace OpenCardMaker.Dialogs
{
    /// <summary>
    /// Interaction logic for AddCard.xaml
    /// </summary>
    public partial class AddCard : Window
    {
        public int target { get; private set; }
        public int skillId { get; private set; }

        public AddCard()
        {
            InitializeComponent();
            CardIDEntry.Text = string.Empty;
        }

        public void btnOKClick(object sender, RoutedEventArgs e)
        {
            if (CardIDEntry.Text.Equals(string.Empty))
            {
                MessageBox.Show("Card ID must not be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int cardId;
            try
            {
                cardId = int.Parse(CardIDEntry.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Parse error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            CardData temp = CardFilesInstance.Instance.QueryCardData(cardId.ToString("D6")); // handle exception later
            var confirm = new AddCardConfirm(temp, CardAssetInstance.Instance.GetImage(cardId.ToString("D6")));
            bool? result = confirm.ShowDialog();

            switch (result)
            {
                case true:
                    target = cardId;
                    skillId = temp.SkillID.id;
                    DialogResult = true;
                    break;
                case false:
                    DialogResult = false;
                    break;
                    // need default switch?
            }

            Close();
        }

        public void btnCancelClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
