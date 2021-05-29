using AssetsTools.NET.Extra;
using OpenCardMaker.Operations.AssetsTools;
using System.Drawing;
using System.IO;

namespace OpenCardMaker.Operations
{
    public class CardAssetInstance
    {
        private static CardAssetInstance instance;
        private static readonly object mutex = new object();

        /// <summary>
        /// Returns active card asset instance.
        /// Set path before calling instance methods.
        /// </summary>
        public static CardAssetInstance Instance
        {
            get
            {
                lock (mutex)
                {
                    if (instance == null) instance = new CardAssetInstance();
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
                if (File.Exists($"{value}\\mu3.exe")) _path = $"{value}\\mu3_Data\\StreamingAssets\\assets";
                else if (File.Exists($"{value}\\package\\mu3.exe")) _path = $"{value}\\package\\mu3_Data\\StreamingAssets\\assets";
                else throw new InvalidPathException();

                isPathSpecified = true;
            }
        }

        public CardAssetInstance()
        {
            isPathSpecified = false;
        }

        /// <summary>
        /// Returns image data from bundle with specified card ID.
        /// </summary>
        /// <param name="cardId">Card ID.</param>
        /// <returns></returns>
        public Bitmap GetImage(string cardId)
        {
            if (!isPathSpecified) throw new UnsetPathException();
            return BitmapExtractor.GetBitmap(_path, cardId);
        }
    }
}