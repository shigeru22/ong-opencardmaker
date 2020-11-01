using OpenCardMaker.Operations;
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

namespace OpenCardMaker.Windows.GodMode
{
    /// <summary>
    /// Interaction logic for GodMain.xaml
    /// </summary>
    public partial class GodMain : Window
    {
        readonly string ongekiPath;
        readonly string configPath;
        CardFilesInstance cardInst;
        UserOperations userOp;
        UserData user;
        UserCard card;

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
        }

        public GodMain(string ongekiPath, string configPath)
        {
            this.ongekiPath = ongekiPath;
            this.configPath = configPath;

            cardInst = new CardFilesInstance(this.ongekiPath);
            userOp = new UserOperations(this.configPath);
            List<CardRow> cardList = new List<CardRow>();

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
            
            foreach(UserCardData data in card.userCardList)
            {
                // query card from xml
                CardRow temp = new CardRow(data.cardId, "", "", data.level, "");
                cardList.Add(temp);
                UserCardListData.Items.Add(temp);
            }
        }
    }
}
