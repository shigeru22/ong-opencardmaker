﻿using OpenCardMaker.Operations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        CardFilesInstance cardInst;
        List<CardRow> cardList = new List<CardRow>();

        public AllCardsWindow(CardFilesInstance instance)
        {
            cardInst = instance;

            cardList.Clear();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            CardData[] cards = cardInst.QueryAllCardData();
            stopwatch.Stop();
            cardList.AddRange(from CardData card in cards
                              let temp = new CardRow(card.dataName.Substring(4), card.CharaID.str, card.Name.str, card.SkillID.str)
                              select temp);

            InitializeComponent();

            AllCardListData.ItemsSource = null;
            AllCardListData.ItemsSource = cardList;

            DiagnosticLoadTime.Text = $"Loaded {cards.Length} in {(float)stopwatch.ElapsedMilliseconds / 1000}s";
        }

        public void BtnClose(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
