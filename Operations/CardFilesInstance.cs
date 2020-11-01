using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace OpenCardMaker.Operations
{
    class CardFilesInstance
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
            if (Directory.Exists($"{path}\\card{cardId}"))
            {
                CardData target = new CardData();
                using (Stream xml = new FileStream($"{path}\\card{cardId}\\Card.xml", FileMode.Open))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(CardData));
                    target = (CardData)serializer.Deserialize(xml);
                }

                return target;
            }
            else throw new InvalidCardIdException();
        }
    }
}
