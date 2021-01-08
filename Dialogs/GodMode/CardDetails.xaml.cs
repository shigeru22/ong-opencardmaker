using System;
using System.Collections.Generic;
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
    /// Interaction logic for CardDetails.xaml
    /// </summary>
    public partial class CardDetails : Window
    {
        public CardDetails(string cardId, CardData target)
        {
            CardIDBox.Text = cardId;
            CardNameBox.Text = target.Name.str;
            CardAttributeBox.Text = target.Attribute;
            CardSkillBox.Text = target.SkillID.str;
            if (target.LicenseID.id == 0) CopyrightBlock.Text = string.Empty;
            else CopyrightBlock.Text = target.LicenseID.str;

            InitializeComponent();
        }
    }
}
