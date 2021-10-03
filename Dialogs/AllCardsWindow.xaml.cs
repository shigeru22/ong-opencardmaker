using OpenCardMaker.Operations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace OpenCardMaker.Dialogs
{
    /// <summary>
    /// Interaction logic for AllCardsWindow.xaml
    /// </summary>
    public partial class AllCardsWindow : Window
    {
        struct CardRow
        {
            public string CardId { get; set; }
            public string CardName { get; set; }
            public string CardTitle { get; set; }
            public string CardSkill { get; set; }

            public CardRow(int id, string name, string title, string skill)
            {
                CardId = id.ToString();
                CardName = name;
                CardTitle = title;
                CardSkill = skill;
            }

            public CardRow(string id, string name, string title, string skill)
            {
                CardId = id;
                CardName = name;
                CardTitle = title;
                CardSkill = skill;
            }
        }

        List<CardRow> cardList = new List<CardRow>();
        double load;

        public AllCardsWindow()
        {
            cardList.Clear();
            InitializeComponent();

            DiagnosticLoadTime.Text = "Loading all cards...";
        }

        // will convert to this instead of blocking UI thread
        public async void LoadDataAsync()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            int total = 0;
            await Task.Run(() =>
            {
                CardData[] cards = CardFilesInstance.Instance.QueryAllCardDataAsync().Result;
                cardList.AddRange(from CardData card in cards
                                  let temp = new CardRow(card.dataName.Substring(4), card.CharaID.str, card.Name.str, card.SkillID.str)
                                  select temp);
                total = cards.Length;
            });
            stopwatch.Stop();

            AllCardListData.ItemsSource = null;
            AllCardListData.ItemsSource = cardList;

            DiagnosticLoadTime.Text = $"Loaded {total} cards in {(float)stopwatch.ElapsedMilliseconds / 1000}s";
        }

        public void LoadData()
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            CardData[] cards = CardFilesInstance.Instance.QueryAllCardData();
            cardList.AddRange(from CardData card in cards
                              let temp = new CardRow(card.dataName.Substring(4), card.CharaID.str, card.Name.str, card.SkillID.str)
                              select temp);
            stopwatch.Stop();

            load = stopwatch.ElapsedMilliseconds / 1000;
        }

        public void UpdateWindow()
        {
            AllCardListData.ItemsSource = null;
            AllCardListData.ItemsSource = cardList;
            DiagnosticLoadTime.Text = $"Loaded {cardList.Count} cards in {load}s";
        }

        public void BtnClose(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
