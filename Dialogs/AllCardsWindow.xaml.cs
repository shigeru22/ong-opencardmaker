using OpenCardMaker.Operations;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

        public AllCardsWindow()
        {
            cardList.Clear();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            CardData[] cards = CardFilesInstance.Instance.QueryAllCardData();
            stopwatch.Stop();
            cardList.AddRange(from CardData card in cards
                              let temp = new CardRow(card.dataName.Substring(4), card.CharaID.str, card.Name.str, card.SkillID.str)
                              select temp);

            InitializeComponent();

            AllCardListData.ItemsSource = null;
            AllCardListData.ItemsSource = cardList;

            DiagnosticLoadTime.Text = $"Loaded {cards.Length} cards in {(float)stopwatch.ElapsedMilliseconds / 1000}s";
        }

        public void BtnClose(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
