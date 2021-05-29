using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace OpenCardMaker.Operations
{
    public class CardFilesInstance
    {
        private static CardFilesInstance instance;
        private static readonly object mutex = new object();

        /// <summary>
        /// Returns active card files instance.
        /// Set path before calling instance methods.
        /// </summary>
        public static CardFilesInstance Instance
        {
            get
            {
                lock(mutex)
                {
                    if (instance == null) instance = new CardFilesInstance();
                    return instance;
                }
            }
        }

        private bool isPathSpecified;
        private string _path;

        public string path
        {
            set
            {
                if (File.Exists($"{value}\\mu3.exe")) _path = $"{value}\\mu3_Data\\StreamingAssets\\GameData\\A000\\card";
                else if (File.Exists($"{value}\\package\\mu3.exe")) _path = $"{value}\\package\\mu3_Data\\StreamingAssets\\GameData\\A000\\card";
                else throw new InvalidPathException();

                isPathSpecified = true;
            }
        }

        public CardFilesInstance()
        {
            isPathSpecified = false;
        }

        public CardData QueryCardData(string path, bool relativeFromCardDir = true)
        {
            if (!isPathSpecified) throw new UnsetPathException();

            if (File.Exists(path) && Path.GetExtension(path).ToLower() == ".xml")
            {
                CardData target = new CardData();
                using (Stream xml = new FileStream(path, FileMode.Open))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(CardData));
                    target = (CardData)serializer.Deserialize(xml);
                }

                return target;
            }
            else throw new InvalidCardIdException(path);
        }

        public CardData QueryCardData(int cardId)
        {
            if (!isPathSpecified) throw new UnsetPathException();

            string temp = cardId.ToString("D6");
            if (Directory.Exists($"{_path}\\card{temp}"))
            {
                CardData target = new CardData();

                using (Stream xml = new FileStream($"{_path}\\card{temp}\\Card.xml", FileMode.Open))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(CardData));
                    target = (CardData)serializer.Deserialize(xml);
                }

                return target;
            }
            else throw new InvalidCardIdException();
        }

        public CardData QueryCardData(string cardId)
        {
            if (!isPathSpecified) throw new UnsetPathException();

            CardData target;
            string temp;

            if (cardId.Substring(0, 4).Equals("card")) temp = cardId.Substring(4);
            else temp = cardId;
            try
            {
                target = QueryCardData(Int32.Parse(temp));
            }
            catch (Exception)
            {
                // throw later
                return new CardData();
            }

            return target;
        }

        public CardData[] QueryAllCardData()
        {
            if (!isPathSpecified) throw new UnsetPathException();

            List<CardData> cards = new List<CardData>();
            string[] cardDirs = Directory.GetDirectories($"{_path}");

            foreach (string cardDir in cardDirs) cards.Add(QueryCardData($"{cardDir}\\Card.xml", false));

            return cards.ToArray();
        }
    }
}
