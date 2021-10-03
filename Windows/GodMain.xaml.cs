using OpenCardMaker.Dialogs;
using OpenCardMaker.Operations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace OpenCardMaker.Windows
{
    /// <summary>
    /// Interaction logic for GodMain.xaml
    /// </summary>
    public partial class GodMain : Window
    {
        struct CardRow
        {
            public string CardId { get; set; }
            public string CardName { get; set; }
            public string CardTitle { get; set; }
            public string CardLevel { get; set; }
            public string CardSkill { get; set; }

            public CardRow(int id, string name, string title, int level, string skill)
            {
                CardId = id.ToString();
                CardName = name;
                CardTitle = title;
                CardLevel = level.ToString();
                CardSkill = skill;
            }

            public CardRow(string id, string name, string title, int level, string skill)
            {
                CardId = id;
                CardName = name;
                CardTitle = title;
                CardLevel = level.ToString();
                CardSkill = skill;
            }
        }

        UserData user;
        UserCard card;

        List<CardRow> cardList = new List<CardRow>();

        void RefreshCardList()
        {
            int total = 0;

            cardList.Clear();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            foreach (CardRow temp in from UserCardData data in card.userCardList
                                     let card = CardFilesInstance.Instance.QueryCardData(data.cardId)
                                     let temp = new CardRow(data.cardId, card.CharaID.str, card.Name.str, data.level, card.SkillID.str)
                                     select temp)
            {
                cardList.Add(temp);
                total++;
            }
            stopwatch.Stop();

            UserCardListData.ItemsSource = null;
            UserCardListData.ItemsSource = cardList;

            DiagnosticLoadTime.Text = $"Loaded {total} cards in {(float)stopwatch.ElapsedMilliseconds / 1000}s";
        }

        void AddCardToList(CardData target)
        {
            cardList.Add(new CardRow(target.dataName.Substring(4), target.CharaID.str, target.Name.str, 1, target.SkillID.str));
            DiagnosticLoadTime.Text = string.Empty;
        }

        public GodMain(string ongekiPath, string configPath)
        {
            CardFilesInstance.Instance.path = ongekiPath;
            CardAssetInstance.Instance.path = ongekiPath;
            UserOperations.Instance.path = configPath;

            try
            {
                user = UserOperations.Instance.GetUserData();
                card = UserOperations.Instance.GetUserCard();
            }
            catch (Exception e)
            {
                MessageBox.Show($"Unhandled exception occured:\n{e.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                var window = new MainWindow();
                window.Show();

                Close();
            }

            InitializeComponent();

            UserName.Text = $"{user.userId}: {user.userData.userName}";
            RefreshCardList();
            SetClosingEvent();
        }

        private void SetClosingEvent()
        {
            Closing -= GodMain_Logout;
            Closing += GodMain_Closing;
        }

        private void SetLogoutEvent()
        {
            Closing += GodMain_Logout;
            Closing -= GodMain_Closing;
        }

        private void UserCardListData_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if(e.AddedCells[0].Item != null) CopyMenuItem.IsEnabled = true;
        }

        private void GodMain_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!UserOperations.Instance.GetUserCard().EqualCheck(card.userCardList))
            {
                var close = MessageBox.Show("Save any changes before closing?", "Exit", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                switch (close)
                {
                    case MessageBoxResult.Yes:
                        UserOperations.Instance.SaveUserCard(card);
                        break;
                    case MessageBoxResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
        }

        private void GodMain_Logout(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!UserOperations.Instance.GetUserCard().EqualCheck(card.userCardList))
            {
                var close = MessageBox.Show("Save any changes before returning?", "Logout", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                switch (close)
                {
                    case MessageBoxResult.Yes:
                        UserOperations.Instance.SaveUserCard(card);
                        break;
                    case MessageBoxResult.Cancel:
                        e.Cancel = true;
                        SetClosingEvent();
                        return;
                }
            }

            var window = new MainWindow();
            window.Show();
        }

        public async void ViewAllCardsClick(object sender, RoutedEventArgs e)
        {
            var window = new AllCardsWindow();
            var loadWindow = new AllCardsLoading();
            loadWindow.LoadingText.Text = "Loading cards list...";
            loadWindow.Show();

            await Task.Run(() => Thread.Sleep(100));

            window.LoadData();
            window.UpdateWindow();

            loadWindow.Close();
            window.Show();
        }

        public void SaveCardsListClick(object sender, RoutedEventArgs e)
        {
            if (UserOperations.Instance.SaveUserCard(card) != 0) MessageBox.Show("Card data saved.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void RevertToSavedClick(object sender, RoutedEventArgs e)
        {
            var confirm = MessageBox.Show("Discard all changes and revert to last saved data?", "Revert to Saved", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            switch (confirm)
            {
                case MessageBoxResult.Yes:
                    card = UserOperations.Instance.GetUserCard();
                    RefreshCardList();
                    break;
            }
        }

        public void CopyDataClick(object sender, RoutedEventArgs e)
        {
            if (UserCardListData.SelectedItem != null)
            {
                CardRow selected = (CardRow)UserCardListData.SelectedItem;
                string output = string.Empty;

                string menu = ((MenuItem)sender).Name;
                if (menu.Contains("CopyID")) output = selected.CardId;
                else if (menu.Contains("CopyName")) output = selected.CardName;
                else if (menu.Contains("CopyTitle")) output = selected.CardTitle;
                else if (menu.Contains("CopyLevel")) output = selected.CardLevel;
                else if (menu.Contains("CopySkill")) output = selected.CardSkill;

                Clipboard.SetText(output);
            }
            else MessageBox.Show("No card selected. Select one before clicking this option.", "Copy", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        public void AboutClick(object sender, RoutedEventArgs e)
        {
            var window = new AboutWindow();
            window.Show();
        }

        public void btnLogout(object sender, RoutedEventArgs e)
        {
            SetLogoutEvent();
            Close();
        }

        public void ExitApplicationClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public void btnAddClick(object sender, RoutedEventArgs e)
        {
            var dialog = new AddCard();
            bool? result = dialog.ShowDialog();

            switch (result)
            {
                case true:
                    // MessageBox.Show(dialog.target.ToString());
                    card.AddCard(dialog.target, dialog.skillId);
                    RefreshCardList();
                    break;
                default:
                    break;
            }
        }

        public void btnRemoveClick(object sender, RoutedEventArgs e)
        {
            CardRow selected = (CardRow)UserCardListData.SelectedItem;

            var result = MessageBox.Show("Confirm deletion?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    int target = int.Parse(selected.CardId);
                    card.RemoveCard(target);
                    RefreshCardList();
                    break;
                case MessageBoxResult.No:
                    break;
                    // put log on default
            }
        }

        public void btnDetailsClick(object sender, RoutedEventArgs e)
        {
            CardRow selected = (CardRow)UserCardListData.SelectedItem;

            CardDetails dialog = new CardDetails(selected.CardId, CardFilesInstance.Instance.QueryCardData(selected.CardId), CardAssetInstance.Instance.GetImage(selected.CardId));
            dialog.ShowDialog();
        }

        public void btnLevelUpClick(object sender, RoutedEventArgs e)
        {
            CardRow selected = (CardRow)UserCardListData.SelectedItem;

            int cardId = 0, level = 0, maxLevel = 0;
            foreach (UserCardData temp in card.userCardList)
            {
                if (temp.cardId.ToString() == selected.CardId)
                {
                    cardId = temp.cardId;
                    level = temp.level;
                    maxLevel = temp.maxLevel;
                    break;
                }
            }

            if (cardId == 0)
            {
                MessageBox.Show("Specified card ID could not be found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (level == maxLevel)
            {
                MessageBox.Show("Maximum level reached for the selected card.\nIncrease the cap by using the Up Cap button.", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            IncreaseCardLevel dialog = new IncreaseCardLevel(level, maxLevel);
            dialog.ShowDialog();

            switch (dialog.DialogResult)
            {
                case true:
                    int length = card.userCardList.Length;
                    for (int i = 0; i < length; i++)
                    {
                        if (card.userCardList[i].cardId == cardId)
                        {
                            card.userCardList[i].level += dialog.levelNumber;
                            break;
                        }
                    }
                    break;
                default: return;
            }

            RefreshCardList();
        }

        public void btnCapUpClick(object sender, RoutedEventArgs e)
        {
            CardRow selected = (CardRow)UserCardListData.SelectedItem;

            int cardId = 0, cap = 0, maxCap = 0;
            foreach (UserCardData temp in card.userCardList)
            {
                if (temp.cardId.ToString() == selected.CardId)
                {
                    cardId = temp.cardId;
                    cap = temp.maxLevel;

                    string rarity = CardFilesInstance.Instance.QueryCardData(cardId).Rarity;

                    switch (rarity)
                    {
                        case "N": maxCap = 100; break;
                        case "R": maxCap = 70; break;
                        case "SR": maxCap = 70; break;
                        case "SSR": maxCap = 70; break;
                        default:
                            MessageBox.Show("Error", "Unrecognized rarity attribute.", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                    }
                }
            }

            if (cardId == 0)
            {
                MessageBox.Show("Specified card ID could not be found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (cap == maxCap)
            {
                MessageBox.Show("The selected card is already in its maximum level cap.", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            IncreaseCardCap dialog = new IncreaseCardCap(cap, maxCap);
            dialog.ShowDialog();

            switch (dialog.DialogResult)
            {
                case true:
                    int length = card.userCardList.Length;
                    for (int i = 0; i < length; i++)
                    {
                        if (card.userCardList[i].cardId == cardId)
                        {
                            card.userCardList[i].maxLevel += dialog.capNumber;
                            break;
                        }
                    }
                    break;
                default: return;
            }

            RefreshCardList();
        }
    }
}
