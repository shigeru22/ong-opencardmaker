using OpenCardMaker.Operations;
using OpenCardMaker.Windows.GodMode;
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
    /// Interaction logic for AddCard.xaml
    /// </summary>
    public partial class AddCard : Window
    {
        public int target { get; private set; }
        public int skillId { get; private set; }

        CardFilesInstance _inst;
        CardAssetInstance _asset;

        public AddCard(ref CardFilesInstance cardFilesInstance, ref CardAssetInstance cardAssetInstance)
        {
            _inst = cardFilesInstance;
            _asset = cardAssetInstance;

            InitializeComponent();
            CardIDEntry.Text = string.Empty;
        }

        public void btnOKClick(object sender, RoutedEventArgs e)
        {
            CustomDialog dialog;
            if(CardIDEntry.Text.Equals(string.Empty))
            {
                dialog = new CustomDialog("Error", "Card ID must not be empty.");
                dialog.ShowDialog();
                return;
            }

            int cardId;
            try
            {
                cardId = int.Parse(CardIDEntry.Text);
            }
            catch (Exception ex)
            {
                dialog = new CustomDialog("Error", $"Parse error: {ex.Message}");
                dialog.ShowDialog();
                return;
            }

            CardData temp = _inst.QueryCardData(cardId.ToString("D6")); // handle exception later
            var confirm = new AddCardConfirm(temp, _asset.GetImage(cardId.ToString("D6")));
            bool? result = confirm.ShowDialog();

            switch(result)
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
