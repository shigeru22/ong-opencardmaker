using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenCardMaker
{
    public partial class UserCard
    {
        public int userId;
        public int length;
        public int nextIndex;
        public UserCardData[] userCardList;

        public bool AddCard(int cardId, int skillId)
        {
            List<UserCardData> temp = new List<UserCardData>();
            foreach (UserCardData card in userCardList)
            {
                if (card.cardId != cardId) temp.Add(card);
                else return false;
            }

            UserCardData target = new UserCardData(); // fix this
            target.cardId = cardId;
            target.digitalStock = 1;
            target.analogStock = 0;
            target.level = 1;
            target.maxLevel = 10;
            target.exp = 0;
            target.printCount = 1;
            target.useCount = 0;
            target.isNew = true;
            target.kaikaDate = "0000-00-00 00:00:00.0";
            target.choKaikaDate = "0000-00-00 00:00:00.0";
            target.skillId = skillId;
            target.isAcquired = true;
            target.created = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.F");

            temp.Add(target);

            Array.Clear(userCardList, 0, userCardList.Length);
            userCardList = temp.ToArray();
            length = temp.Count;

            return true;
        }

        /*
        public void RemoveCard(int index)
        {
            List<UserCardData> temp = new List<UserCardData>();
            foreach (UserCardData card in userCardList) temp.Add(card);

            temp.RemoveAt(index);
            userCardList = temp.ToArray();
            length = temp.Count;
        }
        */

        public int RemoveCard(int cardId)
        {
            List<UserCardData> temp = new List<UserCardData>();

            int index = -1;
            for (int i = 0; i < length; i++)
            {
                if (userCardList[i].cardId != cardId) temp.Add(userCardList[i]);
                else index = i;
            }

            if (index == -1) throw new Operations.InvalidCardIdException();
            else
            {
                Array.Clear(userCardList, 0, userCardList.Length);
                userCardList = temp.ToArray();
                length = temp.Count;
            }

            return index;
        }

        public bool EqualCheck(UserCardData[] data)
        {
            return Enumerable.SequenceEqual(userCardList, data);
        }
    }
}
