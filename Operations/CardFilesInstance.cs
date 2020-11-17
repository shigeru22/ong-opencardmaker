using System;
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
            if (cardId.Substring(0, 4).Equals("card"))
            {
                int temp;
                try
                {
                    temp = int.Parse(cardId.Substring(4));
                }
                catch (Exception)
                {
                    // throw later
                    return new CardData();
                }
                target = QueryCardData(temp);
            }
            else target = QueryCardData(cardId);

            return target;
        }
    }
}
