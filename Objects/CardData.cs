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
}
