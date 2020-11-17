using System;
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
using OpenCardMaker.Dialogs.GodMode;

namespace OpenCardMaker.Windows.GodMode
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
        }

        public void btnLogout(object sender, RoutedEventArgs e)
        {
            // show confirmation dialog later
            // this time, save it and logout

            userOp.SaveUserCard(card);
            Closing += BackToMain;
            Close();
        }

        public void BackToMain(object sender, EventArgs e)
        {
            var window = new MainWindow();
            window.Show();
        }

        public void btnAddClick(object sender, RoutedEventArgs e)
        {
            // show window, select card, add to usercardlistdata and usercardlist
            var dialog = new AddCard(ref cardInst);
            bool? result = dialog.ShowDialog();

            switch(result)
            {
                case true:
                    MessageBox.Show(dialog.target.ToString());
                    card.AddCard(dialog.target, dialog.skillId);
                    RefreshCardList();
                    break;
                default:
                    break;
            }
        }

        // WIP
        public void btnRemoveClick(object sender, RoutedEventArgs e)
        {
            CardRow selected = (CardRow)UserCardListData.SelectedItem;

            var result = MessageBox.Show("Confirm deletion?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    int target;
                    try
                    {
                        target = int.Parse(selected.CardId);
                    }
                    catch(Exception ex)
                    {
                        CustomDialog dialog = new CustomDialog("Error", ex.Message);
                        dialog.ShowDialog();
                        break;
                    }
                    card.RemoveCard(target);
                    RefreshCardList();
                    break;
                case MessageBoxResult.No:
                    break;
                // put log on default
            }
        }
    }
}
