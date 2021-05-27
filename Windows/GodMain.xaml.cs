﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Diagnostics;
using OpenCardMaker.Operations;
using System.Linq;
using OpenCardMaker.Dialogs;

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

        readonly string ongekiPath;
        readonly string configPath;
        CardFilesInstance cardInst;
        CardAssetInstance assetsInst;
        UserOperations userOp;
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
                                 let card = cardInst.QueryCardData(data.cardId)
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
            this.ongekiPath = ongekiPath;
            this.configPath = configPath;

            cardInst = new CardFilesInstance(this.ongekiPath);
            assetsInst = new CardAssetInstance(this.ongekiPath);
            userOp = new UserOperations(this.configPath);

            try
            {
                user = userOp.GetUserData();
                card = userOp.GetUserCard();
            }
            catch (Exception)
            {
                // show dialog later
                var window = new MainWindow();
                window.Show();

                Close();
            }

            InitializeComponent();

            UserName.Text = $"{user.userId}: {user.userData.userName}";
            RefreshCardList();

            Closing += GodMain_Closing;
        }

        private void GodMain_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var close = MessageBox.Show("Exit the application?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch(close)
            {
                case MessageBoxResult.No:
                    e.Cancel = true;
                    break;
                default:
                    // userOp.SaveUserCard(card);
                    break;
            }
        }

        public void btnLogout(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Save to configuration and logout?", "Logout", MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch(result)
            {
                case MessageBoxResult.Yes: break;
                default: return;
            }

            // userOp.SaveUserCard(card);
            Closing += BackToMain;
            Closing -= GodMain_Closing; // probably find elegant way?
            Close();
        }

        public void BackToMain(object sender, EventArgs e)
        {
            var window = new MainWindow();
            window.Show();
        }

        public void ViewAllCardsClick(object sender, RoutedEventArgs e)
        {
            var window = new AllCardsWindow(cardInst);
            window.Show();
        }

        public void SaveCardsListClick(object sender, RoutedEventArgs e)
        {
            if(userOp.SaveUserCard(card) != 0) MessageBox.Show("Card data saved.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void RevertToSavedClick(object sender, RoutedEventArgs e)
        {
            var confirm = MessageBox.Show("Discard all changes and revert to last saved data?", "Revert to Saved", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            switch(confirm)
            {
                case MessageBoxResult.Yes:
                    card = userOp.GetUserCard();
                    RefreshCardList();
                    break;
            }
        }

        public void CopyDataClick(object sender, RoutedEventArgs e)
        {
            CardRow selected = (CardRow)UserCardListData.SelectedItem;
            string output = string.Empty;

            // MessageBox.Show(((MenuItem)sender).Name);
            string menu = ((MenuItem)sender).Name;
            if(menu.Contains("CopyID")) output = selected.CardId;
            else if(menu.Contains("CopyName")) output = selected.CardName;
            else if(menu.Contains("CopyTitle")) output = selected.CardTitle;
            else if(menu.Contains("CopyLevel")) output = selected.CardLevel;
            else if(menu.Contains("CopySkill")) output = selected.CardSkill;

            Clipboard.SetText(output);
        }

        public void ExitApplicationClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public void btnAddClick(object sender, RoutedEventArgs e)
        {
            var dialog = new AddCard(ref cardInst, ref assetsInst);
            bool? result = dialog.ShowDialog();

            switch(result)
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

            CardDetails dialog = new CardDetails(selected.CardId, cardInst.QueryCardData(selected.CardId), assetsInst.GetImage(selected.CardId));
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

            if(cardId == 0)
            {
                MessageBox.Show("Specified card ID could not be found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if(level == maxLevel)
            {
                MessageBox.Show("Maximum level reached for the selected card.\nIncrease the cap by using the Up Cap button.", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            IncreaseCardLevel dialog = new IncreaseCardLevel(level, maxLevel);
            dialog.ShowDialog();

            switch(dialog.DialogResult)
            {
                case true:
                    int length = card.userCardList.Length;
                    for(int i = 0; i < length; i++)
                    {
                        if(card.userCardList[i].cardId == cardId)
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
            foreach(UserCardData temp in card.userCardList)
            {
                if(temp.cardId.ToString() == selected.CardId)
                {
                    cardId = temp.cardId;
                    cap = temp.maxLevel;

                    switch(cardInst.QueryCardData(cardId).Rarity)
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

            if(cap == maxCap)
            {
                MessageBox.Show("The selected card is already in its maximum level cap.", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            IncreaseCardCap dialog = new IncreaseCardCap(cap, maxCap);
            dialog.ShowDialog();

            switch(dialog.DialogResult)
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