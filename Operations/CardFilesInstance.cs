using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace OpenCardMaker.Operations
{
    public class CardFilesInstance
    {
        readonly string path;

        /// <summary>
        /// Creates a card file data instance. Must be instantiated.
        /// </summary>
        /// <param name="path"></param>
        public CardFilesInstance(string path)
        {
            if (File.Exists($"{path}\\mu3.exe")) this.path = $"{path}\\mu3_Data\\StreamingAssets\\GameData\\A000\\card";
            else if (File.Exists($"{path}\\package\\mu3.exe")) this.path = $"{path}\\package\\mu3_Data\\StreamingAssets\\GameData\\A000\\card";
            else throw new InvalidPathException();
        }

        public CardData QueryCardData(string path, bool relativeFromCardDir = true)
        {
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
            string temp = cardId.ToString("D6");
            if (Directory.Exists($"{path}\\card{temp}"))
            {
                CardData target = new CardData();

                using (Stream xml = new FileStream($"{path}\\card{temp}\\Card.xml", FileMode.Open))
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
            List<CardData> cards = new List<CardData>();
            string[] cardDirs = Directory.GetDirectories($"{path}");
            
            foreach(string cardDir in cardDirs) cards.Add(QueryCardData($"{cardDir}\\Card.xml", false));

            return cards.ToArray();
        }
    }
}
