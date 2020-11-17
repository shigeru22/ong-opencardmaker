﻿using System;
using System.Collections.Generic;
using System.Text;

namespace OpenCardMaker
{
    public struct CardData
    {
        public string dataName;
        public IdStr Name;
        public IdStr CharaID;
        public string NickName;
        public int SortOrder;
        public string Attribute;
        public IdStr School;
        public string NameForCommonModel;
        public bool IsCommonModel;
        public bool IsTicketUsable;
        public IdStr Gakunen;
        public IdStr PersonalityIDs; // string? idstr? idk
        public string Rarity;
        public int[] LevelParam;
        public IdStr SkillID;
        public IdStr ChoKaikaSkillID;
        public IdStr CardRightsName;
        public string Version;
        public string ReleaseVersion;
        public string CardNumberString;
        public IdStr LicenseID;
        public bool IsLockedAtTheBeginning;
        public string PrintEndDateTime;
    }

    public struct IdStr
    {
        public int id;
        public string str;
    }

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

    public class User
    {
        public int accessCode;
        public string userName;
        public int level;
        public int reincarnationNum;
        public int exp;
        public int point;
        public int totalPoint;
        public int playCount;
        public int jewelCount;
        public int totalJewelCount;
        public int playerRating;
        public int highestRating;
        public int battlePoint;
        public int nameplateId;
        public int trophyId;
        public int cardId;
        public int characterId;
        public int tabSetting;
        public int tabSortSetting;
        public int cardCategorySetting;
        public int cardSortSetting;
        public int playedTutorialBit;
        public int firstTutorialCancelNum;
        public int sumTechHighScore;
        public int sumTechBasicHighScore;
        public int sumTechAdvancedHighScore;
        public int sumTechExpertHighScore;
        public int sumTechMasterHighScore;
        public int sumTechLunaticHighScore;
        public int sumBattleHighScore;
        public int sumBattleBasicHighScore;
        public int sumBattleAdvancedHighScore;
        public int sumBattleExpertHighScore;
        public int sumBattleMasterHighScore;
        public int sumBattleLunaticHighScore;
        public string eventWatchedDate;
        public string firstGameId;
        public string firstRomVersion;
        public string firstDataVersion;
        public string firstPlayDate;
        public string lastGameId;
        public string lastRomVersion;
        public string lastDataVersion;
        public string compatibleCmVersion;
        public string lastPlayDate;
        public int lastPlaceId;
        public string lastPlaceName;
        public int lastRegionId;
        public string lastRegionName;
        public int lastAllNetId;
        public string lastClientId;
        public int lastUsedDeckId;
        public int lastPlayMusicLevel;
    }

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
            for(int i = 0; i < length; i++)
            {
                if (userCardList[i].cardId != cardId) temp.Add(userCardList[i]);
                else index = i;
            }

            if(index == -1) throw new Operations.InvalidCardIdException();
            else
            {
                Array.Clear(userCardList, 0, userCardList.Length);
                userCardList = temp.ToArray();
                length = temp.Count;
            }

            return index;
        }
    }

    public partial class UserData
    {
        public int userId;
        public User userData;
        public int bpRank;
    }
}
