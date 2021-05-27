using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCardMaker
{
    public struct UserCardData
    {
        public int cardId;
        public int digitalStock;
        public int analogStock;
        public int level;
        public int maxLevel;
        public int exp;
        public int printCount;
        public int useCount;
        public bool isNew;
        public string kaikaDate;
        public string choKaikaDate;
        public int skillId;
        public bool isAcquired;
        public string created;
    }
}
